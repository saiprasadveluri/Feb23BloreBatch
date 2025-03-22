import { ProjectMember } from "./project-member";

export interface Task {
    id: string; 
    title: string; 
    description: string; 
    assignedTo: ProjectMember; 
    status: 'Pending' | 'In Progress' | 'Completed';
  }
  