export interface Task {
    id: string; // Task ID
    name: string; // Task Name
    projectId: string; // Associated Project ID
    assignedTo: string; // User ID of the Developer or Tester
    status: string; // Task Status (e.g., "Pending", "In Progress", "Completed")
  }