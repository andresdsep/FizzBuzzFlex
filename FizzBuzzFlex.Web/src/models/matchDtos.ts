export interface MatchWriteDto {
  id: string;
  gameId: string;
  durationInSeconds: number;
  minimumNumber: number;
  maximumNumber: number;
}

export interface RoundResponseDto {
  roundNumber: number;
  previousRoundResult: boolean | undefined;
  matchId: string;
  promptId: string;
  promptNumber: number;
}

export interface RoundAnswerDto {
  matchId: string;
  promptId: string;
  answer: string;
}
