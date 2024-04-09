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
    public class Comment(string description)
    {
        public string Description { get; set;} = description;
    }
    /// <summary>
    /// Interaction logic for ProfileComments.xaml
    /// </summary>
    public partial class ProfileComments : Window
    {
        public List<Comment> comments = new List<Comment>();
        public ProfileComments()
        {
            comments.Add(new Comment("Nice farm bro! Keep up the good work!"));
            comments.Add(new Comment("Nice farm bro! Keep up the good work!"));
            comments.Add(new Comment("Nice farm bro! Keep up the good work!"));
            comments.Add(new Comment("Wow, so many coins! Aren't you cheating?"));
            comments.Add(new Comment("Nice farm bro! Keep up the good work!"));
            comments.Add(new Comment("Wow, so many coins! Aren't you cheating?"));
            comments.Add(new Comment("Nice farm bro! Keep up the good work!"));
            comments.Add(new Comment("Nice farm bro! Keep up the good work!"));
            this.DataContext = comments;
            InitializeComponent();
        }
    }
}
