using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnapToGrid
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				SnapFile(dialog.FileName);
			}
		}

		private void SnapFile(string path)
		{
			//string name = Path.GetFileNameWithoutExtension(path);
			//parse file and split into separate arrays
			Regex regex = new Regex(@"(\.\d*)");
			string[] fileAsLines = File.ReadAllLines(path);
			int count = 0;
			for (int i = 0; i < fileAsLines.Length; ++i)
			{
				if (fileAsLines[i].Contains("plane") || (fileAsLines[i].Contains("origin")))
				{
					count += regex.Matches(fileAsLines[i]).Count;
					fileAsLines[i] = regex.Replace(fileAsLines[i], "");
				}
			}
			string name = Path.GetDirectoryName(path);
			string fileName = Path.GetFileNameWithoutExtension(path);
			name += "\\" + fileName + "_snapped.vmf";
			File.WriteAllLines(name, fileAsLines);
			MessageBox.Show("Snapped " + count + " vertices to grid");
		}
	}
}