import { Home } from "./components/Home";
import { TodoLayout } from "./components/TodoLayout";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
      path: '/todo-list',
      element: <TodoLayout />
  }
];

export default AppRoutes;
