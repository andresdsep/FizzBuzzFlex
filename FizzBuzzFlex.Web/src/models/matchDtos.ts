export interface MatchWriteDto {
  id: string;
  gameId: string;
  durationInSeconds: number;
  minimumNumber: number;
  maximumNumber: number;
}

export interface RoundResponseDto {
  roundNumber: number;
  previousRoundResult: boolean;
  promptId: string;
  promptNumber: number;
}
