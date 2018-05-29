
using System;
using System.Collections.Generic;
using FlipSideModels;

namespace FlipSideDataAccess
{

    public class DA
    {


        public List<Story> ReadStories(DateTime date)
        {
            return new BaseRepository().Query<Story>("SELECT * FROM story order by DateRan");
        }

        public Story ReadStory(int id)
        {
            return new BaseRepository().QueryFirstOrDefault<Story>(
                "SELECT * FROM story where id=" + id.ToString() + " order by DateRan");
        }

        public int WriteStory(Story story)
        {
            var query = "INSERT INTO [dbo].[story]([dateRan], [slug], [summary], [byline], [lean], [link], [topic]) " +
                        $" VALUES('{story.dateRan}','{story.slug}','{story.summary}','{story.byline}','{story.lean}','{story.link}', '{story.topic}' )";
            new BaseRepository().Query<string>(query);
            return 1;
        }
    }

}

