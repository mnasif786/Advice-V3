﻿using System.Collections.Generic;
using Advice.Domain.Entities;

namespace Advice.Common.Models
{
    public class TaskTimelineModel
    {
       public int Red { get; private set; }
       public int Amber { get; private set; }
       public int Green { get; private set; }
       public int Platinum { get; private set; }
       public int Total { get; private set; }

       public TaskTimelineModel(int red, int amber, int green, int platinum, int total)
       {
            Red = red;
            Amber = amber;
            Green = green;
            Platinum = platinum;
            Total = total;
       }

       public TaskTimelineModel(IEnumerable<Task> taskList) 
           : this(0,0,0,0,0)
       {           
           foreach (var task in taskList)
           {
               if( task.IsOverDue() )          Red++;
               if (task.IsApproachingSLA())    Amber++;                
               if( task.IsWithinSLA() )        Green++;
               if( task.IsJustStarted() )      Platinum ++;

               Total++;
           }           
       }


       public static TaskTimelineModel Create(IEnumerable<Task> taskList)
       {
           var taskTimelineModel = new TaskTimelineModel(0, 0, 0, 0, 0);

           foreach (var task in taskList)
           {
               if (task.IsOverDue()) taskTimelineModel.Red++;
               if (task.IsApproachingSLA()) taskTimelineModel.Amber++;
               if (task.IsWithinSLA()) taskTimelineModel.Green++;
               if (task.IsJustStarted()) taskTimelineModel.Platinum++;

               taskTimelineModel.Total++;
           }

           return taskTimelineModel;
       }      
    }
}