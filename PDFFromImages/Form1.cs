using PdfSharp.Drawing;
using PdfSharp.Pdf;
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

namespace PDFFromImages
{
    public partial class Form1 : Form
    {
        private List<string> imagePaths = new List<string>();
        private readonly string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
        private string lastExportedPdfPath = null;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
            {
                // Optional: Drop shadow for drop panel
                e.Graphics.FillRectangle(shadowBrush, dropPanel.Left + 5, dropPanel.Top + 5, dropPanel.Width, dropPanel.Height);
            }
        }

        private void DropPanel_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                string ext = System.IO.Path.GetExtension(file).ToLower();
                if (allowedExtensions.Contains(ext))
                {
                    imagePaths.Add(file); // (optional: you can remove this since it's now redundant)
                    listBoxImages.Items.Add(new ImageItem { FilePath = file });
                }
            }
        }

        private void DropPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void BtnAddImages_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                ofd.Multiselect = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in ofd.FileNames)
                    {
                        imagePaths.Add(file); // (optional: you can remove this since it's now redundant)
                        listBoxImages.Items.Add(new ImageItem { FilePath = file });
                    }
                }
            }
        }

        private void BtnExportPdf_Click(object sender, EventArgs e)
        {
            if (imagePaths.Count == 0)
            {
                MessageBox.Show("No images to export!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF File|*.pdf";
                sfd.Title = "Save PDF";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        PdfDocument doc = new PdfDocument();

                        foreach (ImageItem item in listBoxImages.Items)
                        {
                            string path = item.FilePath;
                            using (Image img = Image.FromFile(path))
                            {
                                PdfPage page = doc.AddPage();
                                page.Width = XUnit.FromPoint(img.Width);
                                page.Height = XUnit.FromPoint(img.Height);

                                using (XGraphics gfx = XGraphics.FromPdfPage(page))
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                    ms.Position = 0;

                                    XImage xImg = XImage.FromStream(ms);
                                    gfx.DrawImage(xImg, 0, 0, img.Width, img.Height);
                                }
                            }
                        }

                        doc.Save(sfd.FileName);
                        lastExportedPdfPath = sfd.FileName;
                        MessageBox.Show("PDF exported successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to export PDF:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            imagePaths.Clear();
            listBoxImages.Items.Clear();
        }

        private void ListBoxImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxImages.SelectedItem is ImageItem item && File.Exists(item.FilePath))
            {
                picturePreview.Image = Image.FromFile(item.FilePath);
            }
        }

        private void BtnMoveUp_Click(object sender, EventArgs e)
        {
            int index = listBoxImages.SelectedIndex;
            if (index > 0)
            {
                var item = listBoxImages.Items[index];
                listBoxImages.Items.RemoveAt(index);
                listBoxImages.Items.Insert(index - 1, item);
                listBoxImages.SelectedIndex = index - 1;
            }
        }

        private void BtnMoveDown_Click(object sender, EventArgs e)
        {
            int index = listBoxImages.SelectedIndex;
            if (index < listBoxImages.Items.Count - 1 && index >= 0)
            {
                var item = listBoxImages.Items[index];
                listBoxImages.Items.RemoveAt(index);
                listBoxImages.Items.Insert(index + 1, item);
                listBoxImages.SelectedIndex = index + 1;
            }
        }

        private void BtnRemoveSelected_Click(object sender, EventArgs e)
        {
            int index = listBoxImages.SelectedIndex;
            if (index >= 0)
            {
                listBoxImages.Items.RemoveAt(index);
                picturePreview.Image = null;
            }
        }

        //private void BtnEditPdf_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("Edit PDF functionality is not yet implemented. Coming soon!", "Coming Soon", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

        //private void BtnViewPdf_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(lastExportedPdfPath) || !File.Exists(lastExportedPdfPath))
        //    {
        //        MessageBox.Show("No exported PDF found to view.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    var viewerForm = new PdfViewerForm(lastExportedPdfPath);
        //    viewerForm.ShowDialog();
        //}


    }
}
