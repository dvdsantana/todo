import React, { Component } from 'react'
import axios from 'axios'
import update from 'immutability-helper'

class TodosContainer extends Component {
    constructor(props) {
        super(props)
        this.state = {
            todos: []
        }
    }

    getAllTasks() {
        axios.get('/api/tasks')
            .then(response => {
                this.setState({ todos: response.data })
            })
            .catch(error => console.log(error))
    }

    getPendingTasks() {
        axios.get('/api/tasks/pending')
            .then(response => {
                this.setState({ todos: response.data })
            })
            .catch(error => console.log(error))
    }

    getCompletedTasks() {
        axios.get('/api/tasks/completed')
            .then(response => {
                this.setState({ todos: response.data })
            })
            .catch(error => console.log(error))
    }

    createTask = (e) => {
        if (e.key === 'Enter') {
            axios.post('/api/tasks', { description: e.target.value })
                .then(response => {
                    const todos = update(this.state.todos, {
                        // add the new task at the top list
                        $splice: [[0, 0, response.data]]
                    })
                    this.setState({
                        todos: todos
                    })
                })
                .catch(error => console.log(error))
        }
    }

    toggleState = (id) => {
        axios.put(`/api/tasks/${id}`)
            .then(response => {
                const todoIndex = this.state.todos.findIndex(x => x.id === response.data.id)
                const todos = update(this.state.todos, {
                    // update the item after finding its index
                    [todoIndex]: { $set: response.data }
                })
                this.setState({
                    todos: todos
                })
            })
            .catch(error => console.log(error))
    }

    deleteTask = (id) => {
        axios.delete(`/api/tasks/${id}`)
            .then(response => {
                const todoIndex = this.state.todos.findIndex(x => x.id === id)
                const todos = update(this.state.todos, {
                    // remove the item with todoIndex
                    $splice: [[todoIndex, 1]]
                })
                this.setState({
                    todos: todos
                })
            })
            .catch(error => console.log(error))
    }

    componentDidMount() {
        this.getAllTasks()
    }

    render() {
        return (
            <div>
                <div className="inputContainer">
                    <input className="taskInput" type="text"
                        placeholder="Add a task and press Enter" maxLength="250"
                        onKeyPress={this.createTask} />
                </div>
                <div className="filter">
                    <label onClick={() => this.getAllTasks()} title="Show all tasks">All</label>
                    <label onClick={() => this.getPendingTasks()} title="Show pending tasks">Pending</label>
                    <label onClick={() => this.getCompletedTasks()} title="Show completed tasks">Completed</label>
                </div>
                <div>
                    <ul className="taskList">
                        {this.state.todos.map((todo) => {
                            return (
                                <li className={"task" + (todo.isCompleted ? ' completed' : '')}
                                    todo={todo} key={todo.id}>
                                    <label className="taskLabel"
                                        onClick={() => this.toggleState(todo.id)}>
                                        {todo.description}
                                    </label>
                                    <span className="deleteTaskBtn"
                                        onClick={() => this.deleteTask(todo.id)}>x</span>
                                </li>
                            )
                        })}
                    </ul>
                </div>
            </div>
        )
    }
}

export default TodosContainer