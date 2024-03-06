using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AbstractMedia.Core.Models;

namespace AbstractMedia.Core.Context;

public class MediaFileContext : IMediaContext
{
    private readonly string _filePath;
    public List<Media> Media { get; set; }


    public MediaFileContext(string filePath)
    {
        _filePath = filePath;

        // Create the directory and file if they do not exist
        var directory = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (!File.Exists(_filePath))
        {
            File.Create(_filePath).Close();
        }

        Media = LoadDataFromFile() ?? new List<Media>();
    }

    public void AddMedia(Media media)
    {
        Media.Add(media);
    }

    public void DeleteMedia(Media media)
    {
        Media.Remove(media);
    }

    public Media GetMediaById(int id)
    {
        return Media.FirstOrDefault(m => m.Id == id);
    }

    public void SaveChanges()
    {
        SaveDataToFile(Media);
    }

    public void UpdateMedia(Media media)
    {
        var existingMedia = Media.FirstOrDefault(m => m.Id == media.Id);
        if (existingMedia != null)
        {
            var index = Media.IndexOf(existingMedia);
            Media[index] = media;
        }
    }

    private List<Media> LoadDataFromFile()
    {
        var mediaList = new List<Media>();
        var lines = File.ReadAllLines(_filePath).Skip(1);

        foreach (var line in lines)
        {
            var parts = line.Split(',');
            var type = parts[0];
            var id = int.Parse(parts[1]);
            var title = parts[2];
            var season = string.IsNullOrEmpty(parts[3]) ? 0 : int.Parse(parts[3]);
            var episode = string.IsNullOrEmpty(parts[4]) ? 0 : int.Parse(parts[4]);
            var writers = string.IsNullOrEmpty(parts[5]) ? null : parts[5].Split('|');
            var format = parts[6];
            var length = string.IsNullOrEmpty(parts[7]) ? 0 : int.Parse(parts[7]);
            var regions = string.IsNullOrEmpty(parts[8]) ? null : parts[8].Split('|').Select(int.Parse).ToArray();
            var genres = string.IsNullOrEmpty(parts[9]) ? null : parts[9].Split('|');

            switch (type)
            {
                case "Movie":
                    mediaList.Add(new Movie(id, title, genres));
                    break;
                case "Show":
                    mediaList.Add(new Show(id, title, season, episode, writers));
                    break;
                case "Video":
                    mediaList.Add(new Video(id, title, format, length, regions));
                    break;
                default:
                    throw new Exception("Unknown media type");
            }
        }

        return mediaList;
    }

    private void SaveDataToFile(List<Media> media)
    {
        // Instructions:
        // 1. Loop through each media item in the 'media' list.
        // 2. For each media item, convert its properties to a string format suitable for a CSV file.
        //    - For example, if a media item has properties 'Type', 'Id', and 'Title', the string might look like "Movie,1,Toy Story".
        //    - Note: You'll need to handle the 'Writers', 'Regions', and 'Genres' properties differently because they are arrays. You can join the elements of these arrays into a string with elements separated by a pipe character ('|').
        // 3. Write these strings to the CSV file. Each string should be on a new line.
        // 4. Make sure to include a header line at the top of the file with the names of the properties.

        // Your code starts here.
        List<string> finalMedia = new List<string> { "Type,Id,Title,Season,Episode,Writers,Format,Length,Regions,Genres", };


        foreach (Media item in media)
        {
            //Format
            string type = item.GetType().Name;
            string title = item.Title;
            string id = item.Id.ToString();
            string genresString = "", season = "", episode = "", writers = "", format = "", length = "", regions = "";


            if (type == "Movie")
            {
                Movie movieItem = (Movie)item;
                genresString = string.Join("|", movieItem.Genres);

            }
            else if (type == "Show")
            {
                Show showItem = (Show)item;
                season = showItem.Season.ToString();
                writers = string.Join("|", showItem.Writers);
                episode = showItem.Episode.ToString();

            }
            else if (type == "Video")
            {
                Video videoItem = (Video)item;
                format = videoItem.Format;
                length = videoItem.Length.ToString();
                regions = string.Join("|", videoItem.Regions.Select(x => x.ToString()));

            }
            else
            {
                throw new Exception("Unknown media type");
            }

            string line = $"{type},{id},{title},{season},{episode},{writers},{format},{length},{regions},{genresString}";
            finalMedia.Add(line);
        }

        File.WriteAllLines(_filePath, finalMedia);
        // Your code ends here.
    }
}
