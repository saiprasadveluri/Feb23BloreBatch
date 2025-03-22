import { ProjectMember } from './project-member';
import { CommentInfo } from './comment-info';

export interface Task {
  id: string; 
  title: string; 
  description: string; 
  assignedTo: ProjectMember;
  status: 'Pending' | 'In Progress' | 'Completed'; 
  comments?: CommentInfo[]; 
}
