using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BBDD{
    public interface IRequestInfo
    {
        public IEnumerator Login(string username, string password);
        public IEnumerator RegisterUser(string username, string password);
        //MODIFICA LOS PUNTOS
        public IEnumerator UpdateUser(string username, int currentPoints, int global_points);
        public IEnumerator GetMaze();
        public IEnumerator CreateMaze(int seed);
        //PASA LA MAZE DE NO_COMPLETADO A COMPLETADO
        public IEnumerator UpdateMaze(int id);
        public IEnumerator AskIfMazeFinished(int id);
        public IEnumerator GetMessages(int idMaze);
        public IEnumerator CreateMessages(string message, int userId, string position, int idMaze);
        public IEnumerator CreateTraps(int idMaze);
        public IEnumerator GetLeaderboard(GameObject playMenu);
    }
}