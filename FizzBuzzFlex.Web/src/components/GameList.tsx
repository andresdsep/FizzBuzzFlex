import { format } from 'date-fns';
import { useQuery } from 'react-query';
import { getAllGames } from '../utils/apiHelpers';

const GameList = () => {
  const { data: games = [], isLoading } = useQuery(getAllGames([]));
  return isLoading ? (
    <h2>Loading games...</h2>
  ) : (
    <div>
      <h2>Game List</h2>
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Author</th>
            <th>Date</th>
          </tr>
        </thead>
        <tbody>
          {games.map((game) => (
            <tr key={game.id}>
              <td>{game.name}</td>
              <td>{game.author}</td>
              <td>{format(game.createdDate, 'dd MMM yyyy')}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default GameList;
