﻿namespace DbGhost.Build.Extensions
{
    static class GenericExtensions
    {
        public static T Coalesce<T>(this T value, T newValue) where T : class
        {
            return value ?? newValue;
        }

        public static T CoalesceReverse<T>(this T value, T newValue) where T : class
        {
            return newValue ?? value;
        }
    }
}
