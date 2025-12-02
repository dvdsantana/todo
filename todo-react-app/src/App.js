import React, { Component } from 'react';
import './App.css';
import TodosContainer from './components/todos-container/TodosContainer';

class App extends Component {
  render() {
    return (
      <div className="container">
        <div className="header">
          <h1>Gandalf's Todo List</h1>
        </div>
        <TodosContainer />
      </div>
    );
  }
}

export default App;