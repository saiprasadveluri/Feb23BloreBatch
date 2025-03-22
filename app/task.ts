export interface Task {
    id?: string;
    projid: string;
    title: string;
    description: string;
    tasktype: string;
    assignedto: string;
    status: string;
  }