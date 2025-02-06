import { AxiosError } from 'axios';
import { UseQueryOptions } from 'react-query';

export const getReactQueryConfig =
  <TResult, Variables extends unknown[], TError = AxiosError>(
    queryFn: (...args: Variables) => () => TResult | Promise<TResult>,
    contentKey: string,
    endpointConfig?: UseQueryOptions<TResult, TError>,
    ...additionalKeys: string[]
  ) =>
  (
    args: Variables,
    config?: UseQueryOptions<TResult, TError>
  ): UseQueryOptions<TResult, TError> => ({
    queryKey: [contentKey, ...additionalKeys, ...args],
    queryFn: queryFn(...args),
    ...endpointConfig,
    ...config,
  });
