using MTWalk.WPF.Controls.Tree;
using System.IO;

namespace MTWalk.WPF.Settings;

class DirectoryViewModel(IExpertProvider expertProvider, IServiceProvider serviceProvider) : FileBaseViewModel, ITree
{
    public IEnumerable<ITreeNode> Children => field ??= GetChildren().AsEnumerable();

    public FileBaseViewModel[] GetChildren()
        => expertProvider.GetFileInfos(FullName).Select(x =>
            {
                FileBaseViewModel result = x switch
                {
                    DirectoryInfo => serviceProvider.GetRequiredService<DirectoryViewModel>(),
                    FileSystemInfo => serviceProvider.GetRequiredService<FileViewModel>(),
                };
                result.Name = x.Name;
                result.FullName = x.FullName;
                return result;

            }).ToArray();
}
