using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gopher
{
    public partial class frmMBR : Form
    {
        Partition[] partitions;

        public frmMBR(Partition[] partitions)
        {
            this.partitions = partitions;
            InitializeComponent();
        }

        private void frmMBR_Load(object sender, EventArgs e)
        {
            foreach (Partition myPartition in partitions)
            {
                rtbMBR.AppendText(myPartition.name + "\n");
                rtbMBR.AppendText("Cylinder Begin : " + myPartition.cylinderBegin + "\n");
                rtbMBR.AppendText("Cylinder End : " + myPartition.cylinderEnd + "\n");
                rtbMBR.AppendText("Head Begin : " + myPartition.headBegin + "\n");
                rtbMBR.AppendText("Head Begin : " + myPartition.headEnd + "\n");
                rtbMBR.AppendText("Sector Begin : " + myPartition.sectorBegin + "\n");
                rtbMBR.AppendText("Sector End : " + myPartition.sectorEnd + "\n");
                rtbMBR.AppendText("Number of Sectors : " + myPartition.sectorNum + "\n");
                rtbMBR.AppendText("Is The Active Partion : " + myPartition.bootable + "\n");
                rtbMBR.AppendText("----------------------------------------------------" + "\n");
            }
        }
    }
}
