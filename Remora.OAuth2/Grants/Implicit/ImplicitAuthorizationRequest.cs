//
//  ImplicitAuthorizationRequest.cs
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
using System.Net.Http;
using System.Web;
using JetBrains.Annotations;
using Remora.OAuth2.Abstractions;
using Remora.OAuth2.Abstractions.OAuthExtensions;
using Remora.OAuth2.Extensions;
using Remora.Rest.Core;

namespace Remora.OAuth2;

/// <inheritdoc />
[PublicAPI]
public record ImplicitAuthorizationRequest
(
    string ClientID,
    Optional<Uri> RedirectUri = default,
    Optional<string> Scope = default,
    Optional<string> State = default,
    Optional<IReadOnlyList<IAuthorizationRequestExtension>> Extensions = default
) : IImplicitAuthorizationRequest
{
    /// <inheritdoc />
    public HttpRequestMessage ToRequest(Uri authorizationEndpoint)
    {
        var builder = new UriBuilder(authorizationEndpoint);

        var parameters = HttpUtility.ParseQueryString(authorizationEndpoint.Query);
        parameters.Add("response_type", ((IAuthorizationRequest)this).ResponseType);
        parameters.Add("client_id", this.ClientID);
        parameters.Add("redirect_uri", this.RedirectUri);
        parameters.Add("scope", this.Scope);
        parameters.Add("state", this.State);

        if (this.Extensions.IsDefined(out var extensions))
        {
            foreach (var requestExtension in extensions)
            {
                requestExtension.AddParameters(parameters);
            }
        }

        builder.Query = parameters.ToString();
        return new HttpRequestMessage(HttpMethod.Get, builder.Uri);
    }
}
