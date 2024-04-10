using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HarvestHaven
{
    public class MockComment(string description)
    {
        public string Description { get; set;} = description;
    }
    /// <summary>
    /// Interaction logic for ProfileComments.xaml
    /// </summary>
    public partial class ProfileComments : Window
    {
        public List<MockComment> comments = new List<MockComment>();
        public ProfileComments()
        {
            comments.Add(new MockComment("Nice farm bro! Keep up the good work!"));
            comments.Add(new MockComment("Nice farm bro! Keep up the good work!"));
            comments.Add(new MockComment("Nice farm bro! Keep up the good work!"));
            comments.Add(new MockComment("Wow, so many coins! Aren't you cheating?"));
            comments.Add(new MockComment("Nice farm bro! Keep up the good work!"));
            comments.Add(new MockComment("Wow, so many coins! Aren't you cheating?"));
            comments.Add(new MockComment("Nice farm bro! Keep up the good work!"));
            comments.Add(new MockComment("Nice farm bro! Keep up the good work!"));
            this.DataContext = comments;
            InitializeComponent();
        }
    }
}
