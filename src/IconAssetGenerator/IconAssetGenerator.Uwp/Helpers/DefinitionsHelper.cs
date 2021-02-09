using System.Collections.Generic;
using IconAssetGenerator.Uwp.Models;

namespace IconAssetGenerator.Uwp.Helpers
{
    public static class DefinitionsHelper
    {
        static DefinitionsHelper()
        {
            AppleIconDefinitions = GenerateAppleDefinitions();
            AndroidIconDefinitions = GenerateAndroidDefinitions();
        }

        public static IReadOnlyList<IconDefinition> AppleIconDefinitions { get; set; }

        public static IReadOnlyList<IconDefinition> AndroidIconDefinitions { get; set; }

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

        private static IReadOnlyList<IconDefinition> GenerateAndroidDefinitions()
        {
            return new List<IconDefinition>
            {
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_launcher",
                    Scale = "drawable",
                    Width = 114,
                    Height = 114
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_menu",
                    Scale = "drawable",
                    Width = 114,
                    Height = 114
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_stat_notify",
                    Scale = "drawable",
                    Width = 57,
                    Height = 57
                },

                // hdpi
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_launcher",
                    Scale = "drawable-hdpi",
                    Width = 114,
                    Height = 114
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_menu",
                    Scale = "drawable-hdpi",
                    Width = 114,
                    Height = 114
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_stat_notify",
                    Scale = "drawable-hdpi",
                    Width = 57,
                    Height = 57
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_tab",
                    Scale = "drawable-hdpi-v5",
                    Width = 57,
                    Height = 57
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_stat_notify",
                    Scale = "drawable-hdpi-v9",
                    Width = 29,
                    Height = 29
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_menu",
                    Scale = "drawable-hdpi-v9",
                    Width = 85,
                    Height = 85
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_stat_notify",
                    Scale = "drawable-hdpi-v11",
                    Width = 43,
                    Height = 43
                },

                //ldpi
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_launcher",
                    Scale = "drawable-ldpi",
                    Width = 43,
                    Height = 43
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_menu",
                    Scale = "drawable-ldpi",
                    Width = 43,
                    Height = 43
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_tab",
                    Scale = "drawable-ldpi",
                    Width = 29,
                    Height = 29
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_stat_notify",
                    Scale = "drawable-ldpi",
                    Width = 30,
                    Height = 30
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_tab",
                    Scale = "drawable-ldpi-v5",
                    Width = 23,
                    Height = 23
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_stat_notify",
                    Scale = "drawable-ldpi-v9",
                    Width = 29,
                    Height = 29
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_menu",
                    Scale = "drawable-ldpi-v9",
                    Width = 85,
                    Height = 85
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_menu",
                    Scale = "drawable-ldpi-v11",
                    Width = 22,
                    Height = 22
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_stat_notify",
                    Scale = "drawable-ldpi-v11",
                    Width = 22,
                    Height = 22
                },

                //mdpi
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_launcher",
                    Scale = "drawable-mdpi",
                    Width = 57,
                    Height = 57
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_menu",
                    Scale = "drawable-mdpi",
                    Width = 57,
                    Height = 57
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_tab",
                    Scale = "drawable-mdpi",
                    Width = 38,
                    Height = 38
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_stat_notify",
                    Scale = "drawable-mdpi",
                    Width = 30,
                    Height = 30
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_tab",
                    Scale = "drawable-mdpi-v5",
                    Width = 35,
                    Height = 35
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_stat_notify",
                    Scale = "drawable-mdpi-v9",
                    Width = 19,
                    Height = 19
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_menu",
                    Scale = "drawable-mdpi-v9",
                    Width = 57,
                    Height = 57
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_menu",
                    Scale = "drawable-mdpi-v11",
                    Width = 29,
                    Height = 29
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_stat_notify",
                    Scale = "drawable-mdpi-v11",
                    Width = 29,
                    Height = 29
                },

                //xhdpi
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_launcher",
                    Scale = "drawable-xhdpi",
                    Width = 114,
                    Height = 114
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_menu",
                    Scale = "drawable-xhdpi",
                    Width = 114,
                    Height = 114
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "btn_stat_notify",
                    Scale = "drawable-xhdpi",
                    Width = 76,
                    Height = 76
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_stat_notify",
                    Scale = "drawable-xhdpi",
                    Width = 59,
                    Height = 59
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_stat_notify",
                    Scale = "drawable-xhdpi-v9",
                    Width = 38,
                    Height = 38
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_menu",
                    Scale = "drawable-xhdpi-v9",
                    Width = 114,
                    Height = 114
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_menu",
                    Scale = "drawable-xhdpi-v11",
                    Width = 57,
                    Height = 57
                },
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_stat_notify",
                    Scale = "drawable-xhdpi-v11",
                    Width = 57,
                    Height = 57
                },
                
                //xxhdpi
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_launcher",
                    Scale = "drawable-xxhdpi",
                    Width = 170,
                    Height = 170
                },

                //xxxhdpi
                new IconDefinition
                {
                    PlatformName = "Android",
                    Category = "ic_launcher",
                    Scale = "drawable-xxxhdpi",
                    Width = 227,
                    Height = 227
                }
            };
        }
    }
}