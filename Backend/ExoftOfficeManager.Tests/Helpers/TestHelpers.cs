using System.Threading.Tasks;

namespace ExoftOfficeManager.Tests.Helpers
{
    class CommitHelper
    {
        public static async void MockedCommit()
        {
            await Task.Run(() => { });
        }
    }
}
