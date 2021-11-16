using Xunit;

namespace attachmentPrint.Tests
{
    public class TestImapConnection
    {
        private readonly TestDemo _getattachments2 = new();
        [Theory]
        [InlineData(true, "imap.gmail.com")]

        public void CheckGmailConnectionIsTrue(bool expected, string srv)
        {
            Assert.Equal(expected, _getattachments2.CheckConnection(srv));
        }

        [Theory]
        [InlineData(false, "imap.kakafui.com")]

        public void CheckFakeConnectionIsFalse(bool expected, string srv)
        {
            Assert.Equal(expected, _getattachments2.CheckConnection(srv));
        }
    }
}
