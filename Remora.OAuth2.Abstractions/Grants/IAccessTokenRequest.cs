//
//  IAccessTokenRequest.cs
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
using JetBrains.Annotations;

namespace Remora.OAuth2.Abstractions;

/// <summary>
/// Represents the common public interface of an access token request.
/// </summary>
/// <remarks>
/// This interface is designed to be extended into a specialized access token
/// request adapted for one of the defined OAuth2 grant methods.
/// </remarks>
[PublicAPI]
public interface IAccessTokenRequest
{
    /// <summary>
    /// Gets the requested grant type.
    /// </summary>
    string GrantType { get; }

    /// <summary>
    /// Creates a request URI from the information contained in the request object.
    /// </summary>
    /// <param name="tokenEndpoint">The token endpoint to use when constructing the request URI.</param>
    /// <returns>The constructed URI.</returns>
    Uri ToRequestUri(Uri tokenEndpoint);
}
