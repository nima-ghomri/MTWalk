using MTWalk.WPF.Controls.Tree;
using System.IO;

namespace MTWalk.WPF.Settings;

class DirectoryViewModel(IExpertProvider expertProvider, IServiceProvider serviceProvider) : FileBaseViewModel, ITree
{
    public IEnumerable<ITreeNode> Children => field ??= GetChildren().AsEnumerable();

    public FileBaseViewModel[] GetChildren()
    {
        var expertsFolder = expertProvider.GetExpertsFolder().FullName;
        return expertProvider.GetFileInfos(RelativePath).Select(x =>
                {
                    FileBaseViewModel result = x switch
                    {
                        DirectoryInfo => serviceProvider.GetRequiredService<DirectoryViewModel>(),
                        FileSystemInfo => serviceProvider.GetRequiredService<FileViewModel>(),
                    };
                    result.Name = x.Name;
                    result.RelativePath = Path.GetRelativePath(expertsFolder, x.FullName);
                    return result;

                }).ToArray();
    }
}
