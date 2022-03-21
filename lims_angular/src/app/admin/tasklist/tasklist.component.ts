import { Component, OnInit, OnDestroy, ViewChild } from "@angular/core";
import { Router } from "@angular/router";
import { FormControl } from "@angular/forms";

import { Observable } from "rxjs";
import { map, startWith } from "rxjs/operators";

import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { MatPaginator } from "@angular/material/paginator";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";

import { DeleteConfirmationDialogComponent } from "src/app/components/dialogs/delete-confirmation-dialog/delete-confirmation-dialog.component";

import { TaskManagerService } from "src/app/services/task-manager.service";
import { AuthService } from "src/app/services/auth.service";

import { Task } from "src/app/models/task.model";
import { Workflow } from "src/app/models/workflow.model";

@Component({
    selector: "app-tasklist",
    templateUrl: "./tasklist.component.html",
    styleUrls: ["./tasklist.component.css"],
})
export class TasklistComponent implements OnInit, OnDestroy {
    // tasklist refresh interval in ms
    reloadInterval = 3000;
    tasklistUpdater: ReturnType<typeof setInterval>;
    loadingTasklist: boolean;
    loadingWorkflows: boolean;
    statusMessage: string;

    filter = "";

    filterInput = new FormControl();
    options: string[] = ["SCHEDULED", "CANCELLED"];
    filteredOptions: Observable<string[]>;

    columnNames = ["taskID", "workflowName", "status", "start", "remove"];
    taskList: Task[];
    sortableData = new MatTableDataSource();
    workflows: Workflow[];

    constructor(
        private taskMgr: TaskManagerService,
        private auth: AuthService,
        private router: Router,
        public dialog: MatDialog
    ) {}

    @ViewChild(MatSort, { static: true }) sort: MatSort;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    ngOnInit() {
        this.loadingTasklist = true;
        this.loadingWorkflows = true;
        this.statusMessage = "";

        this.sortableData.data = [];
        this.filteredOptions = this.filterInput.valueChanges.pipe(
            startWith(""),
            map((value) => this.filterOptions(value))
        );

        this.updateTasklist();

        this.tasklistUpdater = setInterval(() => {
            this.updateTasklist();
        }, this.reloadInterval);
    }

    ngAfterViewInit() {
        this.sortableData.paginator = this.paginator;
    }

    updateTasklist(): void {
        if (this.auth.isAuthenticated()) {
            this.taskMgr.getTasks().subscribe(
                (tasks) => {
                    if (tasks.error) {
                        this.statusMessage = tasks.error;
                    } else {
                        if (tasks && tasks.length > 0) {
                            for (let task of tasks) {
                                task["workflowName"] = this.getWorkflowName(task.workflowID);
                            }
                            this.taskList = [...tasks];
                            this.sortableData.data = [...this.taskList];
                            this.sortableData.sort = this.sort;
                            this.statusMessage = "";
                        } else {
                            this.statusMessage = "There are currently no Tasks scheduled";
                        }
                    }
                },
                (err) => {
                    this.statusMessage = "Error retrieving data";
                },
                () => {
                    this.loadingTasklist = false;
                }
            );
            this.taskMgr.getWorkflows().subscribe(
                (workflows) => {
                    if (workflows.error) {
                        console.log(workflows.error);
                    } else {
                        this.workflows = [...workflows];
                    }
                },
                (err) => {
                    console.log(err);
                },
                () => {
                    this.loadingWorkflows = false;
                }
            );
        }
    }

    gotoTaskDetail(id: number) {
        this.router.navigateByUrl("/tasks/detail/" + id);
    }

    gotoWorkflowDetail(id: string) {
        this.router.navigateByUrl("/workflows/detail/" + id);
    }

    getWorkflowName(id: string) {
        return this.taskMgr.getWorkflow(id).name;
    }

    deleteTask(id: string): void {
        const dialogRef = this.dialog.open(DeleteConfirmationDialogComponent, {
            data: { type: "Task" },
        });

        dialogRef.afterClosed().subscribe((confirmDelete) => {
            if (confirmDelete) {
                this.taskMgr.deleteTask(id).subscribe((response) => {
                    this.updateTasklist();
                });
            }
        });
    }

    doFilter(value: string): void {
        this.filter = value;
        this.sortableData.filter = value.trim().toLocaleLowerCase();
    }

    filterOptions(value: string): string[] {
        const filterValue = value.toLowerCase();

        return this.options.filter((option) => option.toLowerCase().includes(filterValue));
    }

    ngOnDestroy() {
        clearInterval(this.tasklistUpdater);
    }
}
