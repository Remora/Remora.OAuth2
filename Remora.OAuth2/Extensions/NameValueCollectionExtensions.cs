//
//  NameValueCollectionExtensions.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) Jarl Gullberg
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
using System.Collections.Specialized;
using System.Linq;
using Remora.Rest.Core;

namespace Remora.OAuth2.Extensions;

/// <summary>
/// Defines extension methods for the <see cref="NameValueCollection"/> class.
/// </summary>
internal static class NameValueCollectionExtensions
{
    /// <summary>
    /// Adds a value to the collection if it is logically present.
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <param name="name">The name of the value.</param>
    /// <param name="value">The value.</param>
    /// <typeparam name="T">The type of the contained value.</typeparam>
    public static void Add<T>(this NameValueCollection collection, string name, Optional<T> value) where T : notnull
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
    public static void Add(this NameValueCollection collection, string name, Optional<IReadOnlyList<string>> value)
    {
        if (!value.HasValue)
        {
            return;
        }

        collection.Add(name, string.Join(' ', value.Value));
    }

    /// <summary>
    /// Converts a <see cref="NameValueCollection"/> to a dictionary.
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <returns>The dictionary.</returns>
    /// <exception cref="InvalidOperationException">Thrown if any encountered key is null.</exception>
    public static IReadOnlyDictionary<string, string> ToDictionary(this NameValueCollection collection)
    {
        return collection.AllKeys.Where(k => k is not null).ToDictionary
        (
            k => k ?? throw new InvalidOperationException(),
            k => collection[k] ?? string.Empty
        );
    }
}
