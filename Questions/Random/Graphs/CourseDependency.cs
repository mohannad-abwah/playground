namespace Graphs
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// From: https://leetcode.com/problems/course-schedule/
    /// 
    /// There are a total of n courses you have to take, labeled from 0 to n-1.
    /// Some courses may have prerequisites, for example to take course 0 you have to first take course 1, which is expressed as a pair: [0,1]
    /// Given the total number of courses and a list of prerequisite pairs, is it possible for you to finish all courses?
    /// 
    /// Note:
    /// 1. The input prerequisites is a graph represented by a list of edges, not adjacency matrices. Read more about how a graph is represented.
    /// 2. You may assume that there are no duplicate edges in the input prerequisites.
    /// </summary>
    [TestClass]
    public class CourseSchedule
    {
        [DataTestMethod]
        [DynamicData(nameof(TestData))]
        public void HasCircularDependency(int numCourses, int[,] prerequisites, bool expected)
        {
            int[,] matrix = new int[numCourses,numCourses];
            for (int i=0; i<prerequisites.GetLength(0); i++)
                matrix[prerequisites[i, 0], prerequisites[i, 1]] = 1;
	
	        bool canFinish = true;
	        bool?[] cache = new bool?[numCourses];
	        bool[] visited = new bool[numCourses];
	        for(int i=0; i<numCourses && canFinish; i++)
	        {
		        canFinish &= CanFinishCourse(i, matrix, visited, cache);
	        }
	
            Assert.AreEqual(expected, canFinish);
        }

        private static bool CanFinishCourse(int course, int[,] matrix, bool[] visited, bool?[] cache)
        {
	        if (cache[course].HasValue)
		        return cache[course].Value;
	
	        visited[course] = true;
	
	        bool canBeFinished = true;
	        for (int i=0; i<matrix.GetLength(1) && canBeFinished; i++)
	        {
		        if (i==course)
			        continue;
		        if (matrix[course, i] == 1)
		        {
			        canBeFinished &= !visited[i] && CanFinishCourse(i, matrix, visited, cache);
		        }
	        }
	        visited[course] = false;
	        cache[course] = canBeFinished;
	
	        return canBeFinished;
        }

        private static IEnumerable<object[]> TestData =>
            new[]
            {
                new object[] {2, new[,]{{1, 0},{0, 1}}, false},
                new object[] {2, new[,]{{1, 0}}, true}
            };
    }
}
