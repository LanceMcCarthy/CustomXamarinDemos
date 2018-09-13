using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Workouts.Portable.Models;

namespace Workouts.Portable.Services
{
    public class DataService : IDisposable
    {
        private HttpClient client;

        private string DistRootUrl = "https://raw.githubusercontent.com/everkinetic/data/develop/dist/";

        public DataService()
        {
            client = new HttpClient { BaseAddress = new Uri(DistRootUrl) };
        }

        public async Task<List<Exercise>> GetAllExercisesAsync()
        {
            using (var request = await client.GetAsync("exercises.json"))
            {
                var json = await request.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<Exercise>>(json);

                foreach (var exercise in result)
                {
                    foreach (var exercisePng in exercise.Pngs)
                    {
                        if (string.IsNullOrEmpty(exercise.RelaxationImageUrl))
                        {
                            exercise.RelaxationImageUrl = $"{DistRootUrl}{exercisePng}";
                        }
                        else
                        {
                            exercise.TensionImageUrl = $"{DistRootUrl}{exercisePng}";
                        }
                    }
                }

                return result;
            }
        }

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.client?.Dispose();
                    this.client = null;
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
