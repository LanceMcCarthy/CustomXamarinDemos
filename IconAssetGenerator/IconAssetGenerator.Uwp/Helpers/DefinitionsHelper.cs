using System.Collections.Generic;
using IconAssetGenerator.Uwp.Models;

namespace IconAssetGenerator.Uwp.Helpers
{
    public static class DefinitionsHelper
    {
        static DefinitionsHelper()
        {
            AppleIconDefinitions = GenerateAppleDefinitions();
        }

        public static IReadOnlyList<IconDefinition> AppleIconDefinitions { get; set; }

        private static IReadOnlyList<IconDefinition> GenerateAppleDefinitions()
        {
            return new List<IconDefinition>
            {
                // --------- iPhone ------------ //
                // iOS 9+10
                new IconDefinition
                {
                    PlatformName = "iPhone-iOS 9+10",
                    Category = "Application Icon",
                    Scale = "3x",
                    Width = 180,
                    Height = 180
                },
                new IconDefinition
                {
                    PlatformName = "iPhone-iOS 9+10",
                    Category = "SpotLight",
                    Scale = "3x",
                    Width = 120,
                    Height = 120
                },
                new IconDefinition
                {
                    PlatformName = "iPhone-iOS 9+10",
                    Category = "Settings",
                    Scale = "3x",
                    Width = 87,
                    Height = 87
                },
                // iOS 7+8
                new IconDefinition
                {
                    PlatformName = "iPhone-iOS 7+8",
                    Category = "Application Icon",
                    Scale = "1x",
                    Width = 60,
                    Height = 60
                },
                new IconDefinition
                {
                    PlatformName = "iPhone-iOS 7+8",
                    Category = "Application Icon",
                    Scale = "2x",
                    Width = 120,
                    Height = 120
                },
                new IconDefinition
                {
                    PlatformName = "iPhone-iOS 7+8",
                    Category = "Spotlight",
                    Scale = "1x",
                    Width = 40,
                    Height = 40
                },
                new IconDefinition
                {
                    PlatformName = "iPhone-iOS 7+8",
                    Category = "Spotlight",
                    Scale = "2x",
                    Width = 80,
                    Height = 80
                },
                // iOS 5+6
                new IconDefinition
                {
                    PlatformName = "iPhone-iOS 5+6",
                    Category = "Application Icon",
                    Scale = "1x",
                    Width = 57,
                    Height = 57
                },
                new IconDefinition
                {
                    PlatformName = "iPhone-iOS 5+6",
                    Category = "Application Icon",
                    Scale = "2x",
                    Width = 114,
                    Height = 114
                },
                new IconDefinition
                {
                    PlatformName = "iPhone-iOS 5+6",
                    Category = "Spotlight",
                    Scale = "1x",
                    Width = 29,
                    Height = 29
                },
                new IconDefinition
                {
                    PlatformName = "iPhone-iOS 5+6",
                    Category = "Spotlight",
                    Scale = "2x",
                    Width = 58,
                    Height = 58
                },
                new IconDefinition
                {
                    PlatformName = "iPhone-iOS 5+6",
                    Category = "Settings",
                    Scale = "1x",
                    Width = 29,
                    Height = 29
                },
                new IconDefinition
                {
                    PlatformName = "iPhone-iOS 5+6",
                    Category = "Settings",
                    Scale = "2x",
                    Width = 58,
                    Height = 58
                },
                // --------- iPad ------------ //
                // iOS 9+10
                new IconDefinition
                {
                    PlatformName = "iPad-iOS 9+10",
                    Category = "Application Icon",
                    Scale = "2x",
                    Width = 167,
                    Height = 167
                },
                new IconDefinition
                {
                    PlatformName = "iPad-iOS 9+10",
                    Category = "Spotlight",
                    Scale = "2x",
                    Width = 120,
                    Height = 120
                },
                new IconDefinition
                {
                    PlatformName = "iPad-iOS 9+10",
                    Category = "Settings",
                    Scale = "2x",
                    Width = 58,
                    Height = 58
                },
                // iOS 7+8
                new IconDefinition
                {
                    PlatformName = "iPad-iOS 7+8",
                    Category = "Application Icon",
                    Scale = "1x",
                    Width = 76,
                    Height = 76
                },
                new IconDefinition
                {
                    PlatformName = "iPad-iOS 7+8",
                    Category = "Application Icon",
                    Scale = "2x",
                    Width = 152,
                    Height = 152
                },
                new IconDefinition
                {
                    PlatformName = "iPad-iOS 7+8",
                    Category = "Spotlight",
                    Scale = "1x",
                    Width = 40,
                    Height = 40
                },
                new IconDefinition
                {
                    PlatformName = "iPad-iOS 7+8",
                    Category = "Spotlight",
                    Scale = "2x",
                    Width = 80,
                    Height = 80
                },
                // iOS 5+6
                new IconDefinition
                {
                    PlatformName = "iPad-iOS 5+6",
                    Category = "Application Icon",
                    Scale = "1x",
                    Width = 72,
                    Height = 72
                },
                new IconDefinition
                {
                    PlatformName = "iPad-iOS 5+6",
                    Category = "Application Icon",
                    Scale = "2x",
                    Width = 144,
                    Height = 144
                },
                new IconDefinition
                {
                    PlatformName = "iPad-iOS 5+6",
                    Category = "Spotlight",
                    Scale = "1x",
                    Width = 50,
                    Height = 50
                },
                new IconDefinition
                {
                    PlatformName = "iPad-iOS 5+6",
                    Category = "Spotlight",
                    Scale = "2x",
                    Width = 100,
                    Height = 100
                },
                new IconDefinition
                {
                    PlatformName = "iPad-iOS 5+6",
                    Category = "Settings",
                    Scale = "1x",
                    Width = 29,
                    Height = 29
                },
                new IconDefinition
                {
                    PlatformName = "iPad-iOS 5+6",
                    Category = "Settings",
                    Scale = "2x",
                    Width = 58,
                    Height = 58
                },
            };
        }
    }
}
