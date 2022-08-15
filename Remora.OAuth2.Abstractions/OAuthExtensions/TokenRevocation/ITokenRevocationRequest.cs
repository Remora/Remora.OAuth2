//
//  ITokenRevocationRequest.cs
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
using System.Net.Http;
using JetBrains.Annotations;
using Remora.Rest.Core;

namespace Remora.OAuth2.Abstractions.OAuthExtensions.TokenRevocation;

/// <summary>
/// Represents a request to revoke an issued token.
/// </summary>
[PublicAPI]
public interface ITokenRevocationRequest
{
    /// <summary>
    /// Gets the token to revoke.
    /// </summary>
    string Token { get; }

    /// <summary>
    /// Gets a value that hints the server as to the type of token submitted for revocation.
    /// </summary>
    Optional<string> TokenTypeHint { get; }

    /// <summary>
    /// Creates an HTTP request from the information contained in the request object.
    /// </summary>
    /// <param name="revocationEndpoint">The revocation endpoint to use when constructing the request URI.</param>
    /// <returns>The constructed URI.</returns>
    HttpRequestMessage ToRequest(Uri revocationEndpoint);
}
