using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Agava.YandexGames.Tests
{
    public class PlayerAccountTests
    {
        [UnitySetUp]
        public IEnumerator InitializeSdk()
        {
            if (!YandexGamesSdk.IsInitialized)
                yield return YandexGamesSdk.Initialize(SdkTests.TrackSuccessCallback);
        }

        [Test]
        public void ShouldNotBeAuthorizedOnStart()
        {
            Assert.IsFalse(PlayerAccount.IsAuthorized);
        }

        [Test]
        public void ShouldNotHaveProfileDataPermission()
        {
            Assert.IsFalse(PlayerAccount.HasPersonalProfileDataPermission);
        }

        [UnityTest]
        public IEnumerator AuthorizeShouldInvokeErrorCallback()
        {
            bool callbackInvoked = false;
            PlayerAccount.Authorize(onErrorCallback: (message) =>
            {
                callbackInvoked = true;
            });

            yield return new WaitForSecondsRealtime(1);

            Assert.IsTrue(callbackInvoked);
        }

        [UnityTest]
        public IEnumerator RequestProfileDataPermissionShouldInvokeErrorCallback()
        {
            bool callbackInvoked = false;
            PlayerAccount.RequestPersonalProfileDataPermission(onErrorCallback: (message) =>
            {
                callbackInvoked = true;
            });

            yield return new WaitForSecondsRealtime(1);

            Assert.IsTrue(callbackInvoked);
        }

        [UnityTest]
        public IEnumerator GetProfileDataShouldInvokeErrorCallback()
        {
            bool callbackInvoked = false;
            PlayerAccount.GetProfileData(null, onErrorCallback: (message) =>
            {
                callbackInvoked = true;
            });

            yield return new WaitForSecondsRealtime(1);

            Assert.IsTrue(callbackInvoked);
        }

        [UnityTest]
        public IEnumerator SetPlayerDataShouldInvokeErrorCallback()
        {
            bool callbackInvoked = false;
            PlayerAccount.SetPlayerData(string.Empty, onErrorCallback: (message) =>
            {
                callbackInvoked = true;
            });

            yield return new WaitForSecondsRealtime(1);

            Assert.IsTrue(callbackInvoked);
        }

        [UnityTest]
        public IEnumerator GetPlayerDataShouldInvokeErrorCallback()
        {
            bool callbackInvoked = false;
            PlayerAccount.GetPlayerData(onErrorCallback: (message) =>
            {
                callbackInvoked = true;
            });

            yield return new WaitForSecondsRealtime(1);

            Assert.IsTrue(callbackInvoked);
        }
    }
}
