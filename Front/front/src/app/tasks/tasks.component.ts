import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { catchError } from 'rxjs';
import { SharedService } from '../shared.service';

@Component({
  selector: 'tasks-component',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})

export class TasksComponent implements OnInit {

  tasks: any = [];
  method: any = "add";
  errorMessage: any = "";
  task: any = {};

  taskForm: FormGroup = new FormGroup({
    "name": new FormControl("", [
      Validators.required,
      Validators.maxLength(20)
    ]),
    "description": new FormControl("", [
      Validators.required,
      Validators.maxLength(200)
    ])
  });

  constructor(private service: SharedService) { }

  ngOnInit(): void {
    this.loadTasks();
  }

  submit() {
    if (this.method === "add")
      this.addTask();

    if (this.method === "update")
      this.updateTask();
  }

  onUpdateClick(taskFromTable: any){
    this.method = "update";
    this.task = taskFromTable;
    this.taskForm.setValue({
      "name": taskFromTable.name,
      "description": taskFromTable.description
    })
    console.log(this.task);
  }

  addTask() {
    this.service.postTask({
      "name": this.taskForm.get("name")?.value,
      "description": this.taskForm.get("description")?.value
    }).subscribe(data => {
      this.taskForm.reset();
      this.loadTasks();
    }, (error) => { this.errorMessage = "Wrong data" });
  }

  updateTask() {
    this.service.putTask(this.task.id, {
      "name": this.taskForm.get("name")?.value,
      "description": this.taskForm.get("description")?.value
    }).subscribe(data => {
      this.taskForm.reset();
      this.method = "add";
      this.task = {};
      this.loadTasks();
    },
      error => {
        this.errorMessage = "Wrong data"
      })
  }

  deleteTask(task: any) {
    this.service.deleteTask(task.id).subscribe(() => {
      this.loadTasks();
    });

  }

  loadTasks() {
    this.service.getTasks().subscribe(data => {
      this.tasks = data;
    })
  }
}
