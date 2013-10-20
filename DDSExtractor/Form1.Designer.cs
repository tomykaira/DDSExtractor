using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace DDSExtractor
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.toDDS = new System.Windows.Forms.TabPage();
            this.convertButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.texturePathBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nameTablePathBox = new System.Windows.Forms.TextBox();
            this.fromDDS = new System.Windows.Forms.TabPage();
            this.updateUpkButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.newDDSBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.upkBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.texturePathBox2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nameTablePathBox2 = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.toDDS.SuspendLayout();
            this.fromDDS.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.toDDS);
            this.tabControl1.Controls.Add(this.fromDDS);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(469, 268);
            this.tabControl1.TabIndex = 5;
            // 
            // toDDS
            // 
            this.toDDS.Controls.Add(this.convertButton);
            this.toDDS.Controls.Add(this.label2);
            this.toDDS.Controls.Add(this.texturePathBox);
            this.toDDS.Controls.Add(this.label1);
            this.toDDS.Controls.Add(this.nameTablePathBox);
            this.toDDS.Location = new System.Drawing.Point(4, 22);
            this.toDDS.Name = "toDDS";
            this.toDDS.Padding = new System.Windows.Forms.Padding(3);
            this.toDDS.Size = new System.Drawing.Size(461, 242);
            this.toDDS.TabIndex = 0;
            this.toDDS.Text = "toDDS";
            this.toDDS.UseVisualStyleBackColor = true;
            // 
            // convertButton
            // 
            this.convertButton.Location = new System.Drawing.Point(380, 213);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(75, 23);
            this.convertButton.TabIndex = 8;
            this.convertButton.Text = "Convert";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += convertButton_Click;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Texture2D";
            // 
            // texturePathBox
            // 
            this.texturePathBox.Location = new System.Drawing.Point(71, 31);
            this.texturePathBox.Name = "texturePathBox";
            this.texturePathBox.Size = new System.Drawing.Size(384, 19);
            this.texturePathBox.TabIndex = 6;
            this.texturePathBox.Click += texturePathBox_Click;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "NameTable";
            // 
            // nameTablePathBox
            // 
            this.nameTablePathBox.Location = new System.Drawing.Point(71, 6);
            this.nameTablePathBox.Name = "nameTablePathBox";
            this.nameTablePathBox.Size = new System.Drawing.Size(384, 19);
            this.nameTablePathBox.TabIndex = 4;
            this.nameTablePathBox.Click += nameTablePathBox_Click;
            // 
            // fromDDS
            // 
            this.fromDDS.Controls.Add(this.label6);
            this.fromDDS.Controls.Add(this.nameTablePathBox2);
            this.fromDDS.Controls.Add(this.updateUpkButton);
            this.fromDDS.Controls.Add(this.label5);
            this.fromDDS.Controls.Add(this.newDDSBox);
            this.fromDDS.Controls.Add(this.label4);
            this.fromDDS.Controls.Add(this.upkBox);
            this.fromDDS.Controls.Add(this.label3);
            this.fromDDS.Controls.Add(this.texturePathBox2);
            this.fromDDS.Location = new System.Drawing.Point(4, 22);
            this.fromDDS.Name = "fromDDS";
            this.fromDDS.Padding = new System.Windows.Forms.Padding(3);
            this.fromDDS.Size = new System.Drawing.Size(461, 242);
            this.fromDDS.TabIndex = 1;
            this.fromDDS.Text = "fromDDS";
            this.fromDDS.UseVisualStyleBackColor = true;
            // 
            // updateUpkButton
            // 
            this.updateUpkButton.Location = new System.Drawing.Point(380, 213);
            this.updateUpkButton.Name = "updateUpkButton";
            this.updateUpkButton.Size = new System.Drawing.Size(75, 23);
            this.updateUpkButton.TabIndex = 14;
            this.updateUpkButton.Text = "Update";
            this.updateUpkButton.UseVisualStyleBackColor = true;
            this.updateUpkButton.Click += new System.EventHandler(this.updateUpkButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "New DDS";
            // 
            // newDDSBox
            // 
            this.newDDSBox.Location = new System.Drawing.Point(71, 56);
            this.newDDSBox.Name = "newDDSBox";
            this.newDDSBox.Size = new System.Drawing.Size(384, 19);
            this.newDDSBox.TabIndex = 12;
            this.newDDSBox.Click += newDDSBox_Click;
            this.newDDSBox.Text = @"C:\Users\寛\Desktop\extract\Imgset_Spray\warai.dds";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "Original UPK";
            // 
            // upkBox
            // 
            this.upkBox.Location = new System.Drawing.Point(71, 6);
            this.upkBox.Name = "upkBox";
            this.upkBox.Size = new System.Drawing.Size(384, 19);
            this.upkBox.TabIndex = 10;
            this.upkBox.Text = @"C:\HanPurple\J_SF2\SFGame\CookedPCConsole\Imgset_Spray.upk";
            this.upkBox.Click += upkBox_Click;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "Texture2D";
            // 
            // texturePathBox2
            // 
            this.texturePathBox2.Location = new System.Drawing.Point(71, 31);
            this.texturePathBox2.Name = "texturePathBox2";
            this.texturePathBox2.Size = new System.Drawing.Size(384, 19);
            this.texturePathBox2.TabIndex = 8;
            this.texturePathBox2.Click += texturePathBox_Click;
            this.texturePathBox2.Text = @"C:\Users\寛\Desktop\extract\Imgset_Spray\1.Texture2D";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "NameTable";
            // 
            // nameTablePathBox2
            // 
            this.nameTablePathBox2.Location = new System.Drawing.Point(71, 81);
            this.nameTablePathBox2.Name = "nameTablePathBox2";
            this.nameTablePathBox2.Size = new System.Drawing.Size(384, 19);
            this.nameTablePathBox2.TabIndex = 15;
            this.nameTablePathBox2.Click += nameTablePathBox_Click;
            this.nameTablePathBox2.Text = @"C:\Users\寛\Desktop\extract\Imgset_Spray\NameTable.txt";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 292);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.toDDS.ResumeLayout(false);
            this.toDDS.PerformLayout();
            this.fromDDS.ResumeLayout(false);
            this.fromDDS.PerformLayout();
            this.ResumeLayout(false);

        }

        void convertButton_Click(object sender, System.EventArgs e)
        {
            Hashtable nameTable = loadNameTable(this.nameTablePathBox.Text);
            Texture2D texture = new Texture2D(nameTable, this.texturePathBox.Text);
            texture.Save();
        }

        private void updateUpkButton_Click(object sender, System.EventArgs e)
        {
            string upkPath = this.upkBox.Text;
            File.Copy(upkPath, upkPath + ".mod", true);
            UPK upk = new UPK(upkPath + ".mod");
            Hashtable nameTable = loadNameTable(this.nameTablePathBox2.Text);
            Texture2D texture = new Texture2D(nameTable, this.texturePathBox2.Text);
            upk.ReplaceTexture(texture, DDSFile.ReadFile(this.newDDSBox.Text));
        }

        private Hashtable loadNameTable(string path)
        {
            Hashtable table = new Hashtable();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // xx = "string"
                    string[] parts = line.Split('=');
                    table.Add(int.Parse(parts[0].Trim()), parts[1].Trim(' ', '\r', '\n', '"'));
                }
            }
            return table;
        }

        private void selectFileWithTextBox(TextBox target, string filter, string title = "Open file")
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = filter;
            dialog.Title = title;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                target.Text = dialog.FileName;
            }
        }

        void texturePathBox_Click(object sender, System.EventArgs e)
        {
            selectFileWithTextBox((TextBox)sender, "Exported texture|*.Texture2D", "Open texture");
        }

        void nameTablePathBox_Click(object sender, System.EventArgs e)
        {
            selectFileWithTextBox((TextBox)sender, "NameTable|NameTable.txt", "Open texture");
        }

        void newDDSBox_Click(object sender, System.EventArgs e)
        {
            selectFileWithTextBox((TextBox)sender, "DDS File|*.dds", "Open DDS texture");
        }

        private void upkBox_Click(object sender, System.EventArgs e)
        {
            selectFileWithTextBox((TextBox)sender, "UPK file|*.upk", "Open upk");
        }

        #endregion

        private TabControl tabControl1;
        private TabPage toDDS;
        private Button convertButton;
        private Label label2;
        private TextBox texturePathBox;
        private Label label1;
        private TextBox nameTablePathBox;
        private TabPage fromDDS;
        private Button updateUpkButton;
        private Label label5;
        private TextBox newDDSBox;
        private Label label4;
        private TextBox upkBox;
        private Label label3;
        private TextBox texturePathBox2;
        private Label label6;
        private TextBox nameTablePathBox2;

    }
}

