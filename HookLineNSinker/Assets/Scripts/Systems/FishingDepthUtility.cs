using UnityEngine;

namespace HookLineNSinker
{
    // Provides shared conversions between fishing tiers and depth values
    public static class FishingDepthUtility
    {
        // Stores the depth value for the top of the fishing area
        public const int TOP_DEPTH = -280;

        // Stores the depth value for Tier 1
        public const int TIER_1_DEPTH = -670;

        // Stores the depth value for Tier 2
        public const int TIER_2_DEPTH = -1060;

        // Stores the depth value for Tier 3
        public const int TIER_3_DEPTH = -1450;

        // Stores the depth value for Tier 4
        public const int TIER_4_DEPTH = -1840;

        // Stores the depth value for Tier 5
        public const int TIER_5_DEPTH = -2230;

        // Stores the depth value for Tier 6
        public const int TIER_6_DEPTH = -2620;

        // Stores the depth value for Tier 7
        public const int TIER_7_DEPTH = -3010;

        // Stores the depth value for Tier 8
        public const int TIER_8_DEPTH = -3400;

        // Converts a fishing tier into its corresponding depth value
        public static int GetDepthValue(FishingDepth tier)
        {
            // Checks which tier was supplied
            switch (tier)
            {
                // Returns the depth assigned to Tier 1
                case FishingDepth.Tier1:
                    return TIER_1_DEPTH;

                // Returns the depth assigned to Tier 2
                case FishingDepth.Tier2:
                    return TIER_2_DEPTH;

                // Returns the depth assigned to Tier 3
                case FishingDepth.Tier3:
                    return TIER_3_DEPTH;

                // Returns the depth assigned to Tier 4
                case FishingDepth.Tier4:
                    return TIER_4_DEPTH;

                // Returns the depth assigned to Tier 5
                case FishingDepth.Tier5:
                    return TIER_5_DEPTH;

                // Returns the depth assigned to Tier 6
                case FishingDepth.Tier6:
                    return TIER_6_DEPTH;

                // Returns the depth assigned to Tier 7
                case FishingDepth.Tier7:
                    return TIER_7_DEPTH;

                // Returns the depth assigned to Tier 8
                case FishingDepth.Tier8:
                    return TIER_8_DEPTH;

                // Reports an invalid tier instead of silently selecting another tier
                default:
                    Debug.LogWarning("An invalid FishingDepth tier was supplied.");

                    // Returns the Tier 1 depth as a safe fallback
                    return TIER_1_DEPTH;
            }
        }

        // Attempts to convert an actual depth value into a fishing tier
        public static bool TryGetTierFromDepth(int depth, out FishingDepth tier)
        {
            // Checks whether the supplied depth matches Tier 1
            if (depth == TIER_1_DEPTH)
            {
                // Assigns Tier 1 to the output parameter
                tier = FishingDepth.Tier1;

                // Reports that the conversion succeeded
                return true;
            }

            // Checks whether the supplied depth matches Tier 2
            if (depth == TIER_2_DEPTH)
            {
                // Assigns Tier 2 to the output parameter
                tier = FishingDepth.Tier2;

                // Reports that the conversion succeeded
                return true;
            }

            // Checks whether the supplied depth matches Tier 3
            if (depth == TIER_3_DEPTH)
            {
                // Assigns Tier 3 to the output parameter
                tier = FishingDepth.Tier3;

                // Reports that the conversion succeeded
                return true;
            }

            // Checks whether the supplied depth matches Tier 4
            if (depth == TIER_4_DEPTH)
            {
                // Assigns Tier 4 to the output parameter
                tier = FishingDepth.Tier4;

                // Reports that the conversion succeeded
                return true;
            }

            // Checks whether the supplied depth matches Tier 5
            if (depth == TIER_5_DEPTH)
            {
                // Assigns Tier 5 to the output parameter
                tier = FishingDepth.Tier5;

                // Reports that the conversion succeeded
                return true;
            }

            // Checks whether the supplied depth matches Tier 6
            if (depth == TIER_6_DEPTH)
            {
                // Assigns Tier 6 to the output parameter
                tier = FishingDepth.Tier6;

                // Reports that the conversion succeeded
                return true;
            }

            // Checks whether the supplied depth matches Tier 7
            if (depth == TIER_7_DEPTH)
            {
                // Assigns Tier 7 to the output parameter
                tier = FishingDepth.Tier7;

                // Reports that the conversion succeeded
                return true;
            }

            // Checks whether the supplied depth matches Tier 8
            if (depth == TIER_8_DEPTH)
            {
                // Assigns Tier 8 to the output parameter
                tier = FishingDepth.Tier8;

                // Reports that the conversion succeeded
                return true;
            }

            // Assigns a default value because no valid tier matched
            tier = FishingDepth.Tier1;

            // Reports that the supplied depth did not match a tier
            return false;
        }

        // Converts an actual depth value into its matching fishing tier
        public static FishingDepth GetTierFromDepth(int depth)
        {
            // Checks whether the depth matches Tier 1
            if (depth == -670)
                return FishingDepth.Tier1;

            // Checks whether the depth matches Tier 2
            if (depth == -1060)
                return FishingDepth.Tier2;

            // Checks whether the depth matches Tier 3
            if (depth == -1450)
                return FishingDepth.Tier3;

            // Checks whether the depth matches Tier 4
            if (depth == -1840)
                return FishingDepth.Tier4;

            // Checks whether the depth matches Tier 5
            if (depth == -2230)
                return FishingDepth.Tier5;

            // Checks whether the depth matches Tier 6
            if (depth == -2620)
                return FishingDepth.Tier6;

            // Checks whether the depth matches Tier 7
            if (depth == -3010)
                return FishingDepth.Tier7;

            // Checks whether the depth matches Tier 8
            if (depth == -3400)
                return FishingDepth.Tier8;

            // Returns Tier 1 if the depth does not match a known tier
            return FishingDepth.Tier1;
        }
    }
}

