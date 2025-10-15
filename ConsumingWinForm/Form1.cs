namespace ConsumingWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7107/");
                //Request head
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage respons = client.GetAsync("api/departments").Result;
                if (respons.IsSuccessStatusCode)
                {
                    List<DepartmentData> result = await respons.Content.ReadAsAsync<List<DepartmentData>>();

                    dataGridView1.DataSource = result;

                }
            }
        }
    }

    public class DepartmentData
    {
        public int departmentId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
