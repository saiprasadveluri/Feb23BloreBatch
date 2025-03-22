interface TaskInfo {
  id: number;
  name: string;
  description: string;
  status: 'Pending' | 'In Progress' | 'Completed';
  comments?: string[];
  assignedTo: string;
}
