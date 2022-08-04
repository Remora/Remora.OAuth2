//
//  DictionaryExtensions.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) 2017 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using Remora.Rest.Core;

namespace Remora.OAuth2.Extensions;

/// <summary>
/// Defines extension methods for the <see cref="IDictionary{TKey, TValue}"/> interface.
/// </summary>
internal static class DictionaryExtensions
{
    /// <summary>
    /// Adds a value to the collection if it is logically present.
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <param name="name">The name of the value.</param>
    /// <param name="value">The value.</param>
    /// <typeparam name="T">The type of the contained value.</typeparam>
    public static void Add<T>(this IDictionary<string, string> collection, string name, Optional<T> value) where T : notnull
    {
        if (!value.HasValue)
        {
            return;
        }

        collection.Add(name, value.Value.ToString() ?? throw new InvalidOperationException());
    }

    /// <summary>
    /// Adds a set of strings as a single space-delimited string to the collection if it is logically present.
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <param name="name">The name of the value.</param>
    /// <param name="value">The value.</param>
    public static void Add(this IDictionary<string, string> collection, string name, Optional<IReadOnlyList<string>> value)
    {
        if (!value.HasValue)
        {
            return;
        }

        collection.Add(name, string.Join(' ', value.Value));
    }

    /// <summary>
    /// Attempts to retrieve the value from the dictionary with the given key.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <param name="value">The value, or an unspecified optional.</param>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    /// <returns>true if a value was retrieved; otherwise, false.</returns>
    public static bool TryGetValue<TKey, TValue>
    (
        this IReadOnlyDictionary<TKey, TValue> dictionary,
        TKey key,
        out Optional<TValue> value
    )
    {
        value = default;

        if (!dictionary.TryGetValue(key, out var realValue))
        {
            return false;
        }

        value = realValue;
        return true;
    }
}
