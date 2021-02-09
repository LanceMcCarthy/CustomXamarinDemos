using System.Collections.Generic;
using CommonHelpers.Common;
using Newtonsoft.Json;

namespace Workouts.Portable.Models
{
    public class Exercise : BindableBase
    {
        private List<string> primaryMuscleGroups;
        private List<string> secondaryMuscleGroups;
        private List<string> equipment;
        private List<string> steps;
        private List<string> tips;
        private List<string> references;
        private List<string> svgs;
        private List<string> pngs;
        private string relaxationImageUrl;
        private string tensionImageUrl;

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("primer")]
        public string Primer { get; set; }

        [JsonProperty("type")]
        public string ExerciseType { get; set; }

        [JsonProperty("primary")]
        public List<string> PrimaryMuscleGroups
        {
            get => primaryMuscleGroups ?? (primaryMuscleGroups = new List<string>());
            set => SetProperty(ref primaryMuscleGroups, value);
        }

        [JsonProperty("secondary")]
        public List<string> SecondaryMuscleGroups
        {
            get => secondaryMuscleGroups ?? (secondaryMuscleGroups = new List<string>());
            set => SetProperty(ref secondaryMuscleGroups, value);
        }

        [JsonProperty("equipment")]
        public List<string> Equipment
        {
            get => equipment ?? (equipment = new List<string>());
            set => SetProperty(ref equipment, value);
        }

        [JsonProperty("steps")]
        public List<string> Steps
        {
            get => steps ?? (steps = new List<string>());
            set => SetProperty(ref steps, value);
        }

        [JsonProperty("tips")]
        public List<string> Tips
        {
            get => tips ?? (tips = new List<string>());
            set => SetProperty(ref tips, value);
        }

        [JsonProperty("references")]
        public List<string> References
        {
            get => references ?? (references = new List<string>());
            set => SetProperty(ref references, value);
        }

        [JsonProperty("svg")]
        public List<string> Svgs
        {
            get => svgs ?? (svgs = new List<string>());
            set => SetProperty(ref svgs, value);
        }

        [JsonProperty("png")]
        public List<string> Pngs
        {
            get => pngs ?? (pngs = new List<string>());
            set => SetProperty(ref pngs, value);
        }

        public string RelaxationImageUrl
        {
            get => relaxationImageUrl;
            set => SetProperty(ref relaxationImageUrl, value);
        }

        public string TensionImageUrl
        {
            get => tensionImageUrl;
            set => SetProperty(ref tensionImageUrl, value);
        }
    }
}
