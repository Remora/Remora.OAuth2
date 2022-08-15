//
//  IAccessTokenResponse.cs
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
using JetBrains.Annotations;
using Remora.Rest.Core;

namespace Remora.OAuth2.Abstractions;

/// <summary>
/// Represents the common public interface of an access token response.
/// </summary>
/// <remarks>
/// This interface is designed to be extended into a specialized access token
/// response adapted for one of the defined OAuth2 grant methods.
/// </remarks>
[PublicAPI]
public interface IAccessTokenResponse
{
    /// <summary>
    /// Gets the access token issued by the authorization server.
    /// </summary>
    string AccessToken { get; }

    /// <summary>
    /// Gets the type of the token issued.
    /// </summary>
    string TokenType { get; }

    /// <summary>
    /// Gets the lifetime of the access token from the point of generation.
    /// </summary>
    Optional<TimeSpan> ExpiresIn { get; }

    /// <summary>
    /// Gets the scope the token encompasses. This value may be omitted if it is
    /// identical to the client's original request.
    /// </summary>
    Optional<IReadOnlyList<string>> Scope { get; }
}
