using System;
using Xunit;

namespace attachmentPrint.Tests
{
    public class TestImapConnection
    {
        private readonly testDemo _getattachments2 = new testDemo();
        [Theory]
        [InlineData(true, "imap.gmail.com")]

        public void checkGmailConnectionIsTrue(bool expected, string srv)
        {
            Assert.Equal(expected, _getattachments2.checkConnection(srv));
        }

        [Theory]
        [InlineData(false, "imap.kakafui.com")]

        public void checkFakeConnectionIsFalse(bool expected, string srv)
        {
            Assert.Equal(expected, _getattachments2.checkConnection(srv));
        }
    }
}
