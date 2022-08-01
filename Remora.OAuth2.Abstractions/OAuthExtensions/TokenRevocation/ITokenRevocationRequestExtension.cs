//
//  ITokenRevocationRequestExtension.cs
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

using System.Collections.Generic;
using JetBrains.Annotations;

namespace Remora.OAuth2.Abstractions.OAuthExtensions.TokenRevocation;

/// <summary>
/// Marks a type as being an access token request extension.
/// </summary>
[PublicAPI]
public interface ITokenRevocationRequestExtension : IRequestExtension
{
    /// <summary>
    /// Adds the record's parameters to the given collection.
    /// </summary>
    /// <param name="collection">The collection.</param>
    void AddParameters(IDictionary<string, string> collection);
}
