//
//  ImplicitAccessTokenResponse.cs
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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using JetBrains.Annotations;
using Remora.OAuth2.Abstractions;
using Remora.OAuth2.Extensions;
using Remora.Rest.Core;

namespace Remora.OAuth2;

/// <inheritdoc />
[PublicAPI]
public record ImplicitAccessTokenResponse
(
    string AccessToken,
    string TokenType,
    Optional<TimeSpan> ExpiresIn = default,
    Optional<IReadOnlyList<string>> Scope = default,
    Optional<string> State = default
) : IImplicitAccessTokenResponse
{
    /// <summary>
    /// Attempts to parse an access token response from the given URI, visited by the user agent.
    /// </summary>
    /// <param name="location">The visited URI.</param>
    /// <param name="response">The parsed response.</param>
    /// <returns>true if a valid response was parsed; otherwise, false.</returns>
    public static bool TryParse(Uri location, [NotNullWhen(true)] out IImplicitAccessTokenResponse? response)
    {
        response = null;

        if (string.IsNullOrEmpty(location.Fragment))
        {
            return false;
        }

        var properties = HttpUtility.ParseQueryString(location.Fragment[1..]).ToDictionary();
        if (!properties.TryGetValue("access_token", out var accessToken))
        {
            return false;
        }

        if (!properties.TryGetValue("token_type", out var tokenType))
        {
            return false;
        }

        Optional<TimeSpan> expiresIn = default;
        if (properties.TryGetValue("expires_in", out var rawExpiresIn))
        {
            if (!double.TryParse(rawExpiresIn, out var expiresInSeconds))
            {
                return false;
            }

            expiresIn = TimeSpan.FromSeconds(expiresInSeconds);
        }

        Optional<IReadOnlyList<string>> scope = default;
        if (properties.TryGetValue("scope", out var rawScope))
        {
            scope = rawScope.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        _ = properties.TryGetValue("state", out Optional<string> state);

        response = new ImplicitAccessTokenResponse(accessToken, tokenType, expiresIn, scope, state);
        return true;
    }
}
