namespace AutoPlayLecture
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.No = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GroupName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LectureName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Progress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button2 = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.selNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.selName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 540);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(280, 48);
            this.button1.TabIndex = 0;
            this.button1.Text = "선택 강의 듣기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar3
            // 
            this.progressBar3.Location = new System.Drawing.Point(15, 679);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(1162, 23);
            this.progressBar3.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("여기어때 잘난체", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(12, 660);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1165, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("여기어때 잘난체", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(12, 705);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1165, 21);
            this.label2.TabIndex = 14;
            this.label2.Text = "label2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(280, 522);
            this.textBox1.TabIndex = 15;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.No,
            this.GroupName,
            this.LectureName,
            this.Progress});
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(301, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(876, 630);
            this.listView1.TabIndex = 16;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // No
            // 
            this.No.Text = "No";
            this.No.Width = 77;
            // 
            // GroupName
            // 
            this.GroupName.Text = "Group Name";
            this.GroupName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GroupName.Width = 260;
            // 
            // LectureName
            // 
            this.LectureName.Text = "Lecture Name";
            this.LectureName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.LectureName.Width = 402;
            // 
            // Progress
            // 
            this.Progress.Text = "Progress";
            this.Progress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Progress.Width = 138;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 594);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(280, 48);
            this.button2.TabIndex = 17;
            this.button2.Text = "강의 듣기 취소";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.selNo,
            this.selName});
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(1183, 12);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(254, 714);
            this.listView2.TabIndex = 18;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // selNo
            // 
            this.selNo.Text = "No";
            this.selNo.Width = 77;
            // 
            // selName
            // 
            this.selName.Text = "Name";
            this.selName.Width = 173;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1449, 738);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar3);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer2;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader No;
        private System.Windows.Forms.ColumnHeader GroupName;
        private System.Windows.Forms.ColumnHeader LectureName;
        private System.Windows.Forms.ColumnHeader Progress;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader selNo;
        private System.Windows.Forms.ColumnHeader selName;
    }
}

