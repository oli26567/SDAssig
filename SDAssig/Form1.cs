using System;
using System.Windows.Forms;
using SDAssig;

namespace SDAssig
{
    public partial class Form1 : Form
    {
		private IDatabaseService db = new DatabaseManager();
		private FileCrawler crawler;

        public Form1()
        {
            InitializeComponent();
            db.Initialize();
			crawler = new FileCrawler(db);
		}

        private void btnIndex_Click(object sender, EventArgs e)
        {
            string path = @"C:\An3\SD\testare";

            db.ClearDatabase();

            if (!System.IO.Directory.Exists(path))
            {
                MessageBox.Show("ERROR: The folder " + path + " does not exist!");
                return;
            }

            var files = System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories);
            MessageBox.Show("Found " + files.Length + " files in folder.");

            crawler.IndexDirectory(path);
            MessageBox.Show("Indexing complete!");
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            string searchTerm = "";

            if (this.Controls.ContainsKey("textSearch")) searchTerm = this.Controls["textSearch"].Text;
            else if (this.Controls.ContainsKey("txtSearch")) searchTerm = this.Controls["txtSearch"].Text;
            else searchTerm = textSearch.Text;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("ERROR: No text!");
                return;
            }

            lstResults.Items.Clear();
            var results = db.SearchFiles(searchTerm);

            foreach (var res in results)
            {
                lstResults.Items.Add(res);
            }
        }

        
    }
}