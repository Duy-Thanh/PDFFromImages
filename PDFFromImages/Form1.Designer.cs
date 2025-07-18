using System;
using System.Drawing;
using System.Windows.Forms;

namespace PDFFromImages
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private ListBox listBoxImages;
        private Button btnAddImages;
        private Button btnExportPdf;
        private Button btnClear;
        private Label lblTitle;
        private Panel dropPanel;
        private PictureBox picturePreview;
        private Button btnMoveUp;
        private Button btnMoveDown;
        private Button btnRemoveSelected;
        private ToolTip toolTip;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBoxImages = new ListBox();
            this.btnAddImages = new Button();
            this.btnExportPdf = new Button();
            this.btnClear = new Button();
            this.btnMoveUp = new Button();
            this.btnMoveDown = new Button();
            this.btnRemoveSelected = new Button();
            this.lblTitle = new Label();
            this.dropPanel = new Panel();
            this.picturePreview = new PictureBox();
            this.toolTip = new ToolTip(this.components);

            this.SuspendLayout();

            // Form Settings
            this.ShowIcon = false;
            this.ClientSize = new Size(1100, 500);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "📄 PDF From Images";
            this.Font = new Font("Segoe UI", 10F);
            this.BackColor = Color.FromArgb(245, 245, 245);

            // Title Label
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(33, 37, 41);
            lblTitle.Location = new Point(30, 15);
            lblTitle.Text = "PDF From Images";

            // Drop Panel
            dropPanel.AllowDrop = true;
            dropPanel.BackColor = Color.White;
            dropPanel.BorderStyle = BorderStyle.FixedSingle;
            dropPanel.Location = new Point(30, 70);
            dropPanel.Size = new Size(520, 160);
            dropPanel.Padding = new Padding(5);
            dropPanel.DragEnter += DropPanel_DragEnter;
            dropPanel.DragDrop += DropPanel_DragDrop;
            dropPanel.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.LightGray, 1))
                    e.Graphics.DrawRectangle(pen, 0, 0, dropPanel.Width - 1, dropPanel.Height - 1);
            };

            Label dropLabel = new Label
            {
                Text = "📂 Drag & Drop Images Here",
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.Gray,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            dropPanel.Controls.Add(dropLabel);

            // ListBox
            listBoxImages.FormattingEnabled = true;
            listBoxImages.ItemHeight = 20;
            listBoxImages.BackColor = Color.White;
            listBoxImages.BorderStyle = BorderStyle.FixedSingle;
            listBoxImages.Location = new Point(30, 250);
            listBoxImages.Size = new Size(520, 190);
            listBoxImages.SelectedIndexChanged += ListBoxImages_SelectedIndexChanged;
            toolTip.SetToolTip(listBoxImages, "Image list (drag to reorder coming soon)");

            // Divider
            Panel divider = new Panel
            {
                BackColor = Color.LightGray,
                Location = new Point(570, 60),
                Size = new Size(1, 390)
            };
            this.Controls.Add(divider);

            // Preview Label
            Label previewLabel = new Label
            {
                Text = "🖼️ Image Preview",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(590, 230),
                Size = new Size(400, 25),
                ForeColor = Color.DimGray
            };
            this.Controls.Add(previewLabel);

            // PictureBox
            picturePreview.BorderStyle = BorderStyle.Fixed3D;
            picturePreview.Location = new Point(590, 260);
            picturePreview.Size = new Size(460, 180);
            picturePreview.SizeMode = PictureBoxSizeMode.Zoom;
            toolTip.SetToolTip(picturePreview, "Preview of selected image");
            this.Controls.Add(picturePreview);

            // Button Styling Helper
            void StyleButton(Button button, string text, Point location, Color color)
            {
                button.Text = text;
                button.Location = location;
                button.Size = new Size(220, 40);
                button.BackColor = color;
                button.ForeColor = Color.White;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(color, 0.2f);
                button.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(color, 0.1f);
                button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                this.Controls.Add(button);
            }

            // Buttons
            StyleButton(btnAddImages, "➕ Add Images", new Point(590, 70), Color.FromArgb(52, 152, 219));
            btnAddImages.Click += BtnAddImages_Click;
            toolTip.SetToolTip(btnAddImages, "Add images from disk");

            StyleButton(btnExportPdf, "📤 Export to PDF", new Point(590, 125), Color.FromArgb(40, 167, 69));
            btnExportPdf.Click += BtnExportPdf_Click;
            toolTip.SetToolTip(btnExportPdf, "Export current image list to a PDF");

            StyleButton(btnClear, "🗑️ Clear All", new Point(590, 180), Color.FromArgb(220, 53, 69));
            btnClear.Click += BtnClear_Click;
            toolTip.SetToolTip(btnClear, "Clear all images");

            StyleButton(btnMoveUp, "⬆️ Move Up", new Point(830, 70), Color.FromArgb(108, 117, 125));
            btnMoveUp.Click += BtnMoveUp_Click;

            StyleButton(btnMoveDown, "⬇️ Move Down", new Point(830, 125), Color.FromArgb(108, 117, 125));
            btnMoveDown.Click += BtnMoveDown_Click;

            StyleButton(btnRemoveSelected, "❌ Remove Selected", new Point(830, 180), Color.FromArgb(255, 100, 100));
            btnRemoveSelected.Click += BtnRemoveSelected_Click;

            // Final Control Additions
            this.Controls.Add(lblTitle);
            this.Controls.Add(dropPanel);
            this.Controls.Add(listBoxImages);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}