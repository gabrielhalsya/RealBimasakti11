using FastReport;
using System.Collections;

namespace DesignFormPM
{
    public partial class FastReportPM : Form
    {
        private Report _report;
        public FastReportPM()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(PMR00200COMMON.Model.PMR00200DummyData.PMR00200PrintDislpayWithBaseHeader());
            _report.RegisterData(loData, "ResponseDataModel");
            _report.Design();
        }

        private void DesignFormPMR_Load(object sender, EventArgs e)
        {
            _report = new Report();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(PMR00600COMMON.DTO_s.Model.PMR00600DummyData.PMR00600PrintDislpayWithBaseHeader());
            _report.RegisterData(loData, "ResponseDataModel");
            _report.Design();
        }
    }
}