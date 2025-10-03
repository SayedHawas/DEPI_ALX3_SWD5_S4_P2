namespace ConsumingConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("---------- Consuming Web API ----------");
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

                    foreach (DepartmentData department in result)
                    {
                        Console.WriteLine(department.ToString());
                    }
                }
                Console.WriteLine("Close CALL ...");

                Console.Write("Enter the ID : ");
                int id;
                int.TryParse(Console.ReadLine(), out id);
                if (id > 0)
                {
                    HttpResponseMessage responsById = client.GetAsync($"api/departments/{id}").Result;
                    if (responsById.IsSuccessStatusCode)
                    {
                        DepartmentData result = await responsById.Content.ReadAsAsync<DepartmentData>();

                        Console.WriteLine(result.ToString());
                    }
                    else if (responsById.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine($"the Department With Id {id} No Found ....");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID ....");
                }




            }
            Console.ReadLine();
        }
    }
}
