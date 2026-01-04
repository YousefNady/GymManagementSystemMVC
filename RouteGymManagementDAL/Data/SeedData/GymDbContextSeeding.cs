using RouteGymManagementDAL.Data.Contexts;
using RouteGymManagementDAL.Entities;
using System.Text.Json;

namespace RouteGymManagementDAL.Data.DataSeed
{
    public static class GymDbContextSeeding
    {

        public static bool SeedData(GymDbContext dbContext)
        {
            try
            {
                var HasPlans = dbContext.Plans.Any();
                var HasCategories = dbContext.Categories.Any();

                if (HasCategories && HasPlans)
                {
                    return false;
                }

                if (!HasPlans)
                {
                    var plans = LoadDateFromJsonFile<Plan>("plans.json");
                    if (plans.Any()) // if there is any data in the list add it to the database
                    {
                        dbContext.Plans.AddRange(plans);
                    }
                }

                if (!HasCategories) // if there is any data in the list add it to the database
                {
                    var Categories = LoadDateFromJsonFile<Category>("categories.json");
                    if (Categories.Any())
                    {
                        dbContext.Categories.AddRange(Categories);
                    }
                }

                return dbContext.SaveChanges() > 0;

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Seeding is Falid: {ex}");
                return false;
            }
        }

        private static List<T> LoadDateFromJsonFile<T>(string fileName)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Files", fileName);

            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException("Could not find file ");
            }

            string jsonData = File.ReadAllText(FilePath);
            var SerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<T>>(jsonData, SerializerOptions) ?? []; // Empty List if null 
        }
    }
}
