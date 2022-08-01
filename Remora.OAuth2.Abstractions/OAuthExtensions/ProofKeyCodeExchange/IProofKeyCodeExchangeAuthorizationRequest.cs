//
//  IProofKeyCodeExchangeAuthorizationRequest.cs
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

using JetBrains.Annotations;
using Remora.Rest.Core;

namespace Remora.OAuth2.Abstractions.OAuthExtensions.ProofKeyCodeExchange;

/// <summary>
/// Defines additional parameters used for OAuth2 flows implementing the Proof
/// Key of Code Exchange extension when requesting authorization.
/// </summary>
[PublicAPI]
public interface IProofKeyCodeExchangeAuthorizationRequest : IAuthorizationRequestExtension
{
    /// <summary>
    /// Gets the code challenge used for the authorization request.
    /// </summary>
    string CodeChallenge { get; }

    /// <summary>
    /// Gets the method used for the generation of the challenge value. Defaults to "plain" if left unspecified.
    /// </summary>
    Optional<string> CodeChallengeMethod { get; }
}
