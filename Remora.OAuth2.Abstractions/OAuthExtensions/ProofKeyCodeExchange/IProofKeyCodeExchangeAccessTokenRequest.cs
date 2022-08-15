//
//  IProofKeyCodeExchangeAccessTokenRequest.cs
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

using JetBrains.Annotations;

namespace Remora.OAuth2.Abstractions.OAuthExtensions.ProofKeyCodeExchange;

/// <summary>
/// Defines additional parameters used for OAuth2 flows implementing the Proof
/// Key of Code Exchange extension when requesting an access token.
/// </summary>
[PublicAPI]
public interface IProofKeyCodeExchangeAccessTokenRequest : IAccessTokenRequestExtension
{
    /// <summary>
    /// Gets the code verifier used for the access token request.
    /// </summary>
    string CodeVerifier { get; }
}
