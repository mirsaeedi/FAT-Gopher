using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32.SafeHandles;

namespace Gopher
{
    public partial class frmMain : Form
    {
        String mainFullPath;
        Stack<string> historyBack = new Stack<string>();
        Stack<string> historyForward = new Stack<string>();

        #region Methods
       
        private void loadDevices(TreeNode root)
        {
            TreeNode driveNode;
            Partition myPartition;
            root.Nodes.Clear();

            DirectoryEntry driveEntry;
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                string driveName = drive.Name.Substring(0, drive.Name.Length - 1);
                driveNode = new TreeNode(driveName);
                root.Nodes.Add(driveNode);

                myPartition = new Partition(driveName);
                driveEntry = new DirectoryEntry(new byte[32], myPartition);
                driveEntry.fullPath = driveNode.Text;
                driveNode.Tag = driveEntry;
                driveEntry.isPartition = true;

                if (drive.DriveType == DriveType.CDRom)
                {
                    myPartition.type = Partition.PartitionType.CD_ROM;
                    driveNode.ImageIndex = trvDirectory.ImageList.Images.IndexOfKey("cd");
                    driveNode.SelectedImageIndex = trvDirectory.ImageList.Images.IndexOfKey("cd");
                }
                else if (drive.DriveType == DriveType.Fixed)
                {
                    myPartition.type = Partition.PartitionType.Fixed;
                    driveNode.ImageIndex = trvDirectory.ImageList.Images.IndexOfKey("partitions");
                    driveNode.SelectedImageIndex = trvDirectory.ImageList.Images.IndexOfKey("partitions");
                }
                else if (drive.DriveType == DriveType.Removable)
                {
                    myPartition.type = Partition.PartitionType.Removable;
                    driveNode.ImageIndex = trvDirectory.ImageList.Images.IndexOfKey("flash");
                    driveNode.SelectedImageIndex = trvDirectory.ImageList.Images.IndexOfKey("flash");
                }

                if (drive.IsReady == true)
                {
                    myPartition.fileSystem = drive.DriveFormat;
                    if (drive.DriveFormat == "FAT32")
                    {
                        driveEntry.firstCluster = myPartition.bootSector.RootClus;
                        DirectoryEntry[] entries = driveEntry.getDirectories();

                        if (driveEntry.hasSubDir())
                            driveNode.Nodes.Add("SE....SE....SE");
                    }
                }
            }
        }

        private bool openPath(String path)
        {
            int i = -1;
            int j = 0;
            String name;
            TreeNode node = null;

            if (path.IndexOf(trvDirectory.Nodes[0].Text) != 0) path = path.Insert(0, trvDirectory.Nodes[0].Text + "\\");
            if (path[path.Length - 1] != '\\') path += "\\";

            while (path.IndexOf('\\', i + 1) > -1)
            {
                int index = path.IndexOf('\\', i + 1);
                j = index - (i + 1);
                name = path.Substring(i + 1, j);
                i = index;

                j = 0;
                if (node == null)
                {
                    foreach (TreeNode myNode in trvDirectory.Nodes)
                    {
                        if (myNode.Text.ToLower() == name.ToLower())
                        {
                            node = myNode;
                            j = 1;
                            break;
                        }
                    }

                    if (j != 1) return false;//TODO: ot handle file not found.
                }
                else
                {
                    foreach (TreeNode myNode in node.Nodes)
                    {
                        if (myNode.Text.ToLower() == name.ToLower())
                        {
                            node = myNode;
                            j = 1;
                            break;
                        }
                    }

                    if (j != 1) return false;//TODO: ot handle file not found.
                }
                //dont expand if it is the target
                if (i != path.Length - 1) node.Expand();
            }

            trvDirectory.Nodes[0].Expand();
            updatelsv(node);
            return true;
        }

        private void safeOpen(String path)
        {
            if (openPath(path))
            {
                setStacks();
                currentPath = path;
            }
        }

        private void runPath(string path)
        {
            if (path == trvDirectory.Nodes[0].Text)
            {
                safeOpen(trvDirectory.Nodes[0].Text);
                return;
            }

            if (path.IndexOf(trvDirectory.Nodes[0].Text) == 0)
                path = path.Substring(path.IndexOf('\\') + 1);

            if (File.Exists(path))
            {
                Process.Start(path);
            }
            else if (Directory.Exists(path))
            {
                DirectoryInfo info = new DirectoryInfo(path);
                String drive="";

                if(path.IndexOf('\\')!=-1)
                    drive=path.Substring(0, path.IndexOf('\\'));
                else
                    drive = path; //partition
                    
                DirectoryEntry part = null;

                foreach (TreeNode node in trvDirectory.Nodes[0].Nodes)
                {
                    if (node.Text.ToLower() == drive.ToLower())
                    {
                        part = (DirectoryEntry)node.Tag;
                        break;
                    }
                }

                if (part != null)
                {
                    if (part.partition.fileSystem != "FAT32")
                        Process.Start(path);
                    else
                        safeOpen(path);
                }
            }
        }

        private void updatelsv(TreeNode root)
        {
            lsvFiles.Items.Clear();

            lblDate.Text = "";
            lblName.Text = "";
            lblSize.Text = "";

            pictureBox1.Image = null;
            trvDirectory.SelectedNode = root;

            if (trvDirectory.Nodes[0] == root)
            {
                btnUp.BackgroundImage = Image.FromFile("Icons\\upD.png");
                btnUp.Enabled = false;

                foreach (TreeNode node in trvDirectory.Nodes[0].Nodes)
                {
                    ListViewItem drive = new ListViewItem(node.Text);
                    drive.Tag = node.Tag;

                    ((DirectoryEntry)drive.Tag).fullPath = node.Text;
                    ((DirectoryEntry)drive.Tag).attribute = DirectoryEntry.attributes.Directory;

                    if (((DirectoryEntry)drive.Tag).partition.type == Partition.PartitionType.CD_ROM)
                        drive.ImageIndex = lsvFiles.LargeImageList.Images.IndexOfKey("cd");
                    else if (((DirectoryEntry)drive.Tag).partition.type == Partition.PartitionType.Fixed)
                        drive.ImageIndex = lsvFiles.LargeImageList.Images.IndexOfKey("partitions");
                    else if (((DirectoryEntry)drive.Tag).partition.type == Partition.PartitionType.Removable)
                        drive.ImageIndex = lsvFiles.LargeImageList.Images.IndexOfKey("flash");

                    lsvFiles.Items.Add(drive);
                }
            }
            else
            {
                btnUp.BackgroundImage = Image.FromFile("Icons\\up.png");
                btnUp.Enabled = true;

                if (trvDirectory.Nodes[0].Nodes.Contains(root))
                    if (((DirectoryEntry)(root.Tag)).partition.fileSystem != "FAT32") return;

                DirectoryEntry entry = (DirectoryEntry)root.Tag;
                Partition tstPartition = entry.partition;
                DirectoryEntry[] entries = entry.getDirectories();

                for (int i = 2; i < entries.Length; i++)
                {
                    ListViewItem item = new ListViewItem(entries[i].Name);
                    lsvFiles.Items.Add(item);
                    entries[i].fullPath = root.FullPath.Substring(root.FullPath.IndexOf("\\") + 1) + "\\" + entries[i].Name;
                    item.Tag = entries[i];
                    FileInfo fileInf = new FileInfo(entries[i].fullPath);

                    if (fileInf.Extension == "")
                        item.ImageIndex = lsvFiles.LargeImageList.Images.IndexOfKey("folder");
                    else
                    {
                        String txt = fileInf.Extension.Substring(1).ToLower();
                        item.ImageIndex = lsvFiles.LargeImageList.Images.IndexOfKey(getIconKey(txt));
                    }
                }
            }
        }

        private void setStacks()
        {
            String fullPath = currentPath;
            if (fullPath.IndexOf(trvDirectory.Nodes[0].Text) != 0) fullPath = fullPath.Insert(0, trvDirectory.Nodes[0].Text + "\\");
            if (historyBack.Count == 0 || historyBack.Peek() != fullPath)
            {
                historyBack.Push(fullPath);
                historyForward.Clear();
                btnForward.BackgroundImage = Image.FromFile("Icons\\forwardD.png");
                btnForward.Enabled = false;
                btnBack.BackgroundImage = Image.FromFile("Icons\\back.png");
                btnBack.Enabled = true;
            }
        }

        private String getIconKey(String txt)
        {
            if (txt == "wav" || txt == "mp3" || txt == "wma")
                return "sound.png";
            else if (txt == "ppt")
                return "ppt.png";
            else if (txt == "java")
                return "java.png";
            else if (txt == "cs")
                return "cs.png";
            else if (txt == "doc" || txt == "docx")
                return "doc.png";
            else if (txt == "jpg" || txt == "png" || txt == "bmp" || txt == "jpeg" || txt == "ico" || txt == "gif")
                return "image.png";
            else if (txt == "zip" || txt == "rar" || txt == "tar" || txt == "tar.gz" || txt == "s7")
                return "archive.png";
            else if (txt == "pdf")
                return "pdf.png";
            else if (txt == "txt")
                return "text.png";
            else if (txt == "htm" || txt == "html" || txt == "mht" || txt == "xml")
                return "web.png";
            else if (txt == "xls" || txt == "xlsx")
                return "XL.png";
            else if (txt == "exe")
                return "exe.png";
            else return "generic.png";

        }

        #endregion

        public string currentPath
        {
            get
            {
                return mainFullPath;
            }
            set
            {
                if (value.IndexOf(trvDirectory.Nodes[0].Text) != 0) value = value.Insert(0, trvDirectory.Nodes[0].Text + "\\");
                mainFullPath = value;
                cmbPath.Text = value;
            }
        }

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            images.Images.Add("folder", Image.FromFile("Icons\\folder.png"));
            images.Images.Add("pc",Image.FromFile("Icons\\pc.png"));
            images.Images.Add("partitions",Image.FromFile("Icons\\partitions.png"));
            images.Images.Add("flash", Image.FromFile("Icons\\listView\\flash.png"));
            images.Images.Add("cd", Image.FromFile("Icons\\listView\\cd.png"));

            FileInfo fileImage;
            foreach (string myFile in Directory.GetFiles("Icons\\listView\\"))
            {
                fileImage = new FileInfo(myFile);
                listImages.Images.Add(fileImage.Name, Image.FromFile(myFile));
            }

            listImages.Images.Add("folder", Image.FromFile("Icons\\listView\\folder.png"));
            listImages.Images.Add("partitions", Image.FromFile("Icons\\listView\\partitions.png"));
            listImages.Images.Add("flash", Image.FromFile("Icons\\listView\\flash.png"));
            listImages.Images.Add("cd", Image.FromFile("Icons\\listView\\cd.png"));
            
            lsvFiles.LargeImageList = listImages;
            trvDirectory.ImageList = images;

            TreeNode root = new TreeNode();
            trvDirectory.Nodes.Add(root);
            root.ImageIndex = trvDirectory.ImageList.Images.IndexOfKey("pc");
            root.SelectedImageIndex = trvDirectory.ImageList.Images.IndexOfKey("pc");
            root.Text = Environment.MachineName + " Computer";

            currentPath = root.Text;
            root.Nodes.Add("FARIB!!!");
            root.Expand();
            updatelsv(trvDirectory.Nodes[0]);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            runPath(cmbPath.Text);
        }

        private void trvDirectory_AfterExpand(object sender, TreeViewEventArgs e)
        {
            TreeNode root = e.Node;
            if (!root.Equals(trvDirectory.Nodes[0]))
            {
                DirectoryEntry nodeEntry = (DirectoryEntry)root.Tag;
                Partition myPartition = nodeEntry.partition;
                root.Nodes.Clear();

                DirectoryEntry[] entries = nodeEntry.getDirectories();

                TreeNode node;
                foreach (DirectoryEntry entry in entries)
                {
                    if (entry.attribute == DirectoryEntry.attributes.Directory && entry.Name != "." && entry.Name != "..")
                    {
                        node = new TreeNode(entry.Name);
                        node.Tag = entry;
                        if (entry.hasSubDir()) node.Nodes.Add("SE....SE....SE");
                        root.Nodes.Add(node);
                    }
                }
            }
            else
                loadDevices(trvDirectory.Nodes[0]);
        }

        private void lsvFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = lsvFiles.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                DirectoryEntry entry = (DirectoryEntry)item.Tag;
                runPath(entry.fullPath);
            }
        }
        
        private void cmbPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                runPath(cmbPath.Text);
        }
        
        private void lsvFiles_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ListViewItem item = lsvFiles.SelectedItems[0];
                if (item != null)
                {
                    DirectoryEntry entry = (DirectoryEntry)item.Tag;
                    runPath(entry.fullPath);
                }
            }
            else if (e.KeyChar == (char)Keys.Back)
                if (btnBack.Enabled)btnBack_Click(null, null);
        }
        
        private void btnUp_Click(object sender, EventArgs e)
        {
            int i = currentPath.Length-1;
            while (i > 0)
            {
                if (currentPath[i] == '\\') break;
                i--;
            }
            runPath(currentPath.Substring(0, i ));
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            historyBack.Push(currentPath);
            currentPath = historyForward.Pop();
            openPath(currentPath);
            btnBack.Enabled = true;
            btnBack.BackgroundImage = Image.FromFile("Icons\\back.png");
            if (historyForward.Count == 0)
            {
                btnForward.Enabled = false;
                btnForward.BackgroundImage = Image.FromFile("Icons\\forwardD.png");
            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            historyForward.Push(currentPath);
            currentPath = historyBack.Pop();
            openPath(currentPath);
            btnForward.Enabled = true;
            btnForward.BackgroundImage = Image.FromFile("Icons\\forward.png");
            if (historyBack.Count == 0) 
            {
                btnBack.Enabled = false; 
                btnBack.BackgroundImage = Image.FromFile("Icons\\backD.png"); 
            }
        }

        private void trvDirectory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse )
            {
                TreeNode root = e.Node;
                if (((DirectoryEntry)root.Tag).partition.fileSystem != "FAT32") return;
                safeOpen(root.FullPath);
            }
        }
   
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lsvFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvFiles.SelectedItems.Count == 0) return;
            ListViewItem item = (ListViewItem) lsvFiles.SelectedItems[0];
            pictureBox1.Image = item.ImageList.Images[item.ImageIndex];
            DirectoryEntry entry = (DirectoryEntry) item.Tag;

            if (entry.isPartition)
            {
                if(entry.partition.type==Partition.PartitionType.CD_ROM)
                    lblName.Text = "Name : " + entry.fullPath + "   Type : CD-ROM";
                else if (entry.partition.type == Partition.PartitionType.Fixed)
                    lblName.Text = "Name : " + entry.fullPath + "   Type : Fixed";
                if (entry.partition.type == Partition.PartitionType.Removable)
                    lblName.Text = "Name : " + entry.fullPath + "   Type : Removable";
            }
            else
            {
                lblName.Text = "Name : " + entry.Name + "   Type : " + (new FileInfo(entry.fullPath)).Extension + "     Attribute : " + File.GetAttributes(entry.fullPath);
                if (entry.attribute == DirectoryEntry.attributes.Directory) lblSize.Text = "Size : - ";
                else lblSize.Text = "Size : " + entry.FileSize + " bytes";
                lblDate.Text = "Creation date : " + entry.CreationDate + "      Last acces date : " + entry.LastAccessDate + "       Write date : " + entry.writeDate;
            }
            
        }

        private void aboutSEExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAboutprogram myForm = new frmAboutprogram();
            myForm.ShowDialog();
        }

        private void aboutUSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAboutUs myForm = new frmAboutUs();
            myForm.ShowDialog();
        }

        private void showMBRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMBR myform = new frmMBR(SectorReader.readMBR(0));
            myform.ShowDialog();
        }

        private void trvDirectory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return) trvDirectory.SelectedNode.Expand();
        }

        private void trvDirectory_DoubleClick(object sender, EventArgs e)
        {
            if (trvDirectory.SelectedNode != trvDirectory.Nodes[0])
            {
                DirectoryEntry entry = (DirectoryEntry)trvDirectory.SelectedNode.Tag;
                if (entry.isPartition && entry.partition.fileSystem != "FAT32")
                    runPath(entry.fullPath);
            }
        }     
    }
}