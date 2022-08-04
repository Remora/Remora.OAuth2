//
//  ImplicitAccessTokenErrorResponse.cs
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
using System.Diagnostics.CodeAnalysis;
using System.Web;
using JetBrains.Annotations;
using Remora.OAuth2.Abstractions;
using Remora.OAuth2.Extensions;
using Remora.Rest.Core;

namespace Remora.OAuth2;

/// <inheritdoc />
[PublicAPI]
public record ImplicitAccessTokenErrorResponse
(
    string Error,
    Optional<string> ErrorDescription = default,
    Optional<Uri> ErrorUri = default,
    Optional<string> State = default
) : IImplicitAccessTokenErrorResponse
{
    /// <summary>
    /// Attempts to parse an access token error response from the given URI, visited by the user agent.
    /// </summary>
    /// <param name="location">The visited URI.</param>
    /// <param name="response">The parsed response.</param>
    /// <returns>true if a valid response was parsed; otherwise, false.</returns>
    public static bool TryParse(Uri location, [NotNullWhen(true)] out IImplicitAccessTokenErrorResponse? response)
    {
        response = null;

        if (string.IsNullOrEmpty(location.Fragment))
        {
            return false;
        }

        var properties = HttpUtility.ParseQueryString(location.Fragment[1..]).ToDictionary();
        if (!properties.TryGetValue("error", out var error))
        {
            return false;
        }

        _ = properties.TryGetValue("error_description", out Optional<string> errorDescription);
        _ = properties.TryGetValue("error_uri", out Optional<string> rawErrorUri);

        Optional<Uri> errorUri = default;
        if (rawErrorUri.HasValue)
        {
            if (!Uri.TryCreate(rawErrorUri.Value, UriKind.RelativeOrAbsolute, out var parsedErrorUri))
            {
                return false;
            }

            errorUri = parsedErrorUri;
        }

        _ = properties.TryGetValue("state", out Optional<string> state);

        response = new ImplicitAccessTokenErrorResponse(error, errorDescription, errorUri, state);
        return true;
    }
}
