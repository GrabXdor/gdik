using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace GDIK
{
    public partial class Form1 : Form
    {
        private List<Dictionary<string, string>> _replacements = new ();
        private Queue<string> _history = new();

        public Form1() {
            InitializeComponent();
            InitializeDictionary();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            
        }

        private void btnRun_Click(object sender, EventArgs e) {
            var text = textBox.Text;
            var dict = _replacements[comboBox1.SelectedIndex];
            
            var affected = AppLogic.DoReplacement(dict, text, out var result);
            if (affected) {
                _history.Enqueue(text);
                textBox.Text = result;
                btnCancel.Enabled = true;
            }
        }

        private void InitializeDictionary() {
            try {
                var s = File.ReadAllText("dict.json");
                _replacements = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(s);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deserializing JSON: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            if (_history.Count == 0) return;
            var text = _history.Dequeue();
            btnCancel.Enabled = _history.Count > 0;
            textBox.Text = text;
        }
    }
}